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
    public partial class Announcements : System.Web.UI.Page
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
            }
            else
            {
                UpdatePanel updatePanel = Page.Master.FindControl("UpdatePanel1") as UpdatePanel;
                UpdatePanelControlTrigger trigger = new PostBackTrigger();
                trigger.ControlID = upload.UniqueID;
                updatePanel.Triggers.Add(trigger);
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
            if (image.HasFile)
            {
                try
                {
                    var container = client.GetContainerReference(companyName.ToLower());
                    var blob = container.GetBlockBlobReference("images/" + image.FileName);
                    blob.UploadFromStream(image.FileContent);
                    uri = new Uri("http://versolstore.blob.core.windows.net/" + companyName.ToLower() + "/images/" + image.FileName);
                    RegisterAsyncTask(new PageAsyncTask(UpdateSyndicationFeed));

                }
                catch { }
            }
        }
        private async Task UpdateSyndicationFeed()
        {
            //See if feed is already in the cache
            List<SyndicationItem> items = Cache["news"] as List<SyndicationItem>;
            if (items == null)
            {
                var container = client.GetContainerReference(companyName.ToLower());
                var blob = container.GetBlockBlobReference("news/" + "news.rss");
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
                    
                    SyndicationItem item = new SyndicationItem();


                    //Create syndication Item
                    item.Title = TextSyndicationContent.CreatePlaintextContent(name.Text);
                    item.PublishDate = DateTimeOffset.Now;
                    item.Links.Add(SyndicationLink.CreateAlternateLink(uri));
                    item.Summary = TextSyndicationContent.CreatePlaintextContent(description.Text);
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
                Cache.Insert("news",
                    items,
                    null,
                    DateTime.Now.AddHours(1.0),
                    TimeSpan.Zero);
            }
        }
    }
}