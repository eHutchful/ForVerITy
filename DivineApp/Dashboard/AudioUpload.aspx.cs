using DivineApp.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace DivineApp.Dashboard
{
    public partial class AudioUpload : System.Web.UI.Page
    {
        private CloudBlobClient client;
        private string companyName;
        private Uri uri;
        private Uri uri2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Sign_in.aspx");
            }else
            {
                UpdatePanel updatePanel = Page.Master.FindControl("UpdatePanel1") as UpdatePanel;
                UpdatePanelControlTrigger trigger = new PostBackTrigger();
                trigger.ControlID = upload.UniqueID;                
                updatePanel.Triggers.Add(trigger);
                trigger = new PostBackTrigger();
                trigger.ControlID = btnVUpload.UniqueID;
                this.AsyncMode = true;
                var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
                client = storageAccount.CreateCloudBlobClient();
                string CurrentUserId = HttpContext.Current.User.Identity.GetUserId();
                var manager = Context.GetOwinContext().GetUserManager<CompanyUserManager>();
                var user = manager.FindById(CurrentUserId);
                companyName = user.CompanyName;
            }
           
        }
        protected void upload_Click(object sender, EventArgs e)
        {
            if (upload_file.HasFile)
            {
                try
                { 
                    var container = client.GetContainerReference(companyName.ToLower());
                    var blob = container.GetBlockBlobReference("audios/"+upload_file.FileName);
                    blob.UploadFromStream(upload_file.FileContent);
                    uri = new Uri("http://versolstore.blob.core.windows.net/" + companyName.ToLower() + "/audios/" + upload_file.FileName);
                    if (album_art.HasFile)
                    {
                        blob = container.GetBlockBlobReference("images/" + album_art.FileName);
                        blob.UploadFromStream(album_art.FileContent);
                        uri2 = new Uri("http://versolstore.blob.core.windows.net/" + companyName.ToLower() + "/images/" + album_art.FileName);
                        
                    }

                    RegisterAsyncTask(new PageAsyncTask(UpdateSyndicationFeed));
                    
                }
                catch { }
            }
            else
            {

            }
        }
        private async Task UpdateSyndicationFeed()
        {
            //See if feed is already in the cache
            List<SyndicationItem> items = Cache["AudiosFeed"] as List<SyndicationItem>;
            if(items == null)
            {
                var container = client.GetContainerReference(companyName.ToLower());
                var blob = container.GetBlockBlobReference("audios/"+"audio.rss");
                using (MemoryStream ms = new MemoryStream())
                {
                    await blob.DownloadToStreamAsync(ms);
                    StreamReader reader = new StreamReader(ms);
                    if (reader != null)
                    {
                        reader.BaseStream.Position = 0;
                    }
                    SyndicationFeed audiosList = SyndicationFeed.Load(XmlReader.Create(ms));
                    items = audiosList.Items.ToList<SyndicationItem>();
                    //Custom Tags
                    string prefix = "itunes";
                    XmlQualifiedName nam = new XmlQualifiedName(prefix, "http://www.w3.org/2000/xmlns/");
                    XNamespace itunesNs = "http://www.itunes.com/dtds/podcast-1.0.dtd";
                    audiosList.AttributeExtensions.Add(nam, itunesNs.NamespaceName);
                    SyndicationItem item = new SyndicationItem();
                   

                    //Create syndication Item
                    item.Title = TextSyndicationContent.CreatePlaintextContent(name.Text);
                    item.PublishDate = DateTimeOffset.Now;
                    item.Links.Add(SyndicationLink.CreateAlternateLink(uri));
                    item.ElementExtensions.Add(new SyndicationElementExtension("author", itunesNs.NamespaceName, name.Text));
                    item.ElementExtensions.Add(new SyndicationElementExtension("explicit", itunesNs.NamespaceName, "no"));
                    item.ElementExtensions.Add(new SyndicationElementExtension("summary", itunesNs.NamespaceName, description.Text));
                    item.ElementExtensions.Add(new SyndicationElementExtension("subtitle", itunesNs.NamespaceName, subtitle.Text));
                    item.ElementExtensions.Add(
                           new XElement(itunesNs + "image", new XAttribute("href",uri2.ToString())));
                    item.Summary = TextSyndicationContent.CreatePlaintextContent(description.Text);
                    item.Categories.Add(new SyndicationCategory(category.Text));
                    SyndicationLink enclosure = SyndicationLink.CreateMediaEnclosureLink(uri, "audio/mpeg", upload_file.PostedFile.ContentLength); 
                    item.ElementExtensions.Add(new SyndicationElementExtension("subtitle", itunesNs.NamespaceName, subtitle.Text));
                    item.Links.Add(enclosure);

                    //create author Info
                    SyndicationPerson authInfo = new SyndicationPerson();
                    authInfo.Name = authorName.Text;
                    item.Authors.Add(authInfo);
                    items.Add(item);

                    //recreate Feed
                    audiosList.Items = items;
                    blob.Delete();

                    MemoryStream ms1 = new MemoryStream();
                    XmlWriter feedWriter = XmlWriter.Create(ms1);
                    Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(audiosList);
                    rssFormatter.WriteTo(feedWriter);
                    feedWriter.Close();

                    await blob.UploadFromByteArrayAsync(ms1.ToArray(), 0, ms1.ToArray().Length);
                    ms1.Close();
                }
                Cache.Insert("AudiosFeed",
                    items,
                    null,
                    DateTime.Now.AddHours(1.0),
                    TimeSpan.Zero);
            }
        }
        protected void btnVUpload_Click(object sender, EventArgs e)
        {
            if (videoUpload.HasFile)
            {
                try
                {
                    var container = client.GetContainerReference(companyName.ToLower());
                    var blob = container.GetBlockBlobReference("videos/" + videoUpload.FileName);
                    blob.UploadFromStream(videoUpload.FileContent);
                    uri = new Uri("http://versolstore.blob.core.windows.net/" + companyName.ToLower() + "/videos/" + videoUpload.FileName);
                    if (vImageUpload.HasFile)
                    {
                        blob = container.GetBlockBlobReference("images/" + vImageUpload.FileName);
                        blob.UploadFromStream(vImageUpload.FileContent);
                        uri2 = new Uri("http://versolstore.blob.core.windows.net/" + companyName.ToLower() + "/videos/" + vImageUpload.FileName);
                    }
                    RegisterAsyncTask(new PageAsyncTask(UpdateVideoSyndicationFeed));

                }
                catch { }
            }
            else
            {

            }
        }
        private async Task UpdateVideoSyndicationFeed()
        {
            //See if feed is already in the cache
            List<SyndicationItem> items = Cache["VideosFeed"] as List<SyndicationItem>;
            if (items == null)
            {
                var container = client.GetContainerReference(companyName.ToLower());
                var blob = container.GetBlockBlobReference("videos/" + "video.rss");
                using (MemoryStream ms = new MemoryStream())
                {
                    await blob.DownloadToStreamAsync(ms);
                    StreamReader reader = new StreamReader(ms);
                    if (reader != null)
                    {
                        reader.BaseStream.Position = 0;
                    }
                    SyndicationFeed videosList = SyndicationFeed.Load(XmlReader.Create(ms));
                    items = videosList.Items.ToList<SyndicationItem>();
                    //Custom Tags
                    string prefix = "itunes";
                    XmlQualifiedName nam = new XmlQualifiedName(prefix, "http://www.w3.org/2000/xmlns/");
                    XNamespace itunesNs = "http://www.itunes.com/dtds/podcast-1.0.dtd";
                    videosList.AttributeExtensions.Add(nam, itunesNs.NamespaceName);
                    SyndicationItem item = new SyndicationItem();


                    //Create syndication Item
                    item.Title = TextSyndicationContent.CreatePlaintextContent(txtVTitle.Text);
                    item.PublishDate = DateTimeOffset.Now;
                    item.Links.Add(SyndicationLink.CreateAlternateLink(uri));
                    item.ElementExtensions.Add(new SyndicationElementExtension("author", itunesNs.NamespaceName, txtVAuthor.Text));
                    item.ElementExtensions.Add(new SyndicationElementExtension("explicit", itunesNs.NamespaceName, "no"));
                    item.ElementExtensions.Add(new SyndicationElementExtension("summary", itunesNs.NamespaceName, txtVDescription.Text));
                    item.ElementExtensions.Add(new SyndicationElementExtension("subtitle", itunesNs.NamespaceName, txtVSubtitle.Text));
                    item.ElementExtensions.Add(
                           new XElement(itunesNs + "image", new XAttribute("href", uri2.ToString())));
                    item.Summary = TextSyndicationContent.CreatePlaintextContent(txtVDescription.Text);
                    item.Categories.Add(new SyndicationCategory(txtVCategory.Text));
                    SyndicationLink enclosure = SyndicationLink.CreateMediaEnclosureLink(uri, "video/mp4", upload_file.PostedFile.ContentLength);
                    item.ElementExtensions.Add(new SyndicationElementExtension("subtitle", itunesNs.NamespaceName, txtVSubtitle.Text));
                    item.Links.Add(enclosure);

                    //create author Info
                    SyndicationPerson authInfo = new SyndicationPerson();
                    authInfo.Name = txtVAuthor.Text;
                    item.Authors.Add(authInfo);
                    items.Add(item);

                    //recreate Feed
                    videosList.Items = items;
                    blob.Delete();

                    MemoryStream ms1 = new MemoryStream();
                    XmlWriter feedWriter = XmlWriter.Create(ms1);
                    Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(videosList);
                    rssFormatter.WriteTo(feedWriter);
                    feedWriter.Close();

                    await blob.UploadFromByteArrayAsync(ms1.ToArray(), 0, ms1.ToArray().Length);
                    ms1.Close();
                }
                Cache.Insert("VideosFeed",
                    items,
                    null,
                    DateTime.Now.AddHours(1.0),
                    TimeSpan.Zero);
            }
        }
    }
}