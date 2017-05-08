using DivineApp.App_Start;
using DivineApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Xml;

namespace DivineApp.Accounts
{
    public partial class Confirm : System.Web.UI.Page
    {
        public CloudBlobClient client { get; private set; }
        public CompanyUser user { get; private set; }
        protected string StatusMessage
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string code = IdentityHelper.GetCodeFromRequest(Request);
            string userId = IdentityHelper.GetUserIdFromRequest(Request);
            var manager = Context.GetOwinContext().GetUserManager<CompanyUserManager>();
            user = manager.FindById(userId);

            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            client = storageAccount.CreateCloudBlobClient();
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(user.CompanyName.ToLower());
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            if (code != null && userId != null)
            {                
                var result = manager.ConfirmEmail(userId, code);
                if (result.Succeeded)
                {
                    successPanel.Visible = true;
                    createAudioSyndicationFeed();
                    createVideoSyndicationFeed();
                    Response.Redirect("~/Sign_in.aspx");
                    return;
                }
            }
            successPanel.Visible = false;
            errorPanel.Visible = true;
        }

        private void createAudioSyndicationFeed()
        {
            //Create syndicated feed
            var myFeed = new SyndicationFeed();
            myFeed.Title = TextSyndicationContent.CreatePlaintextContent(user.CompanyName);
            myFeed.Description = TextSyndicationContent.CreatePlaintextContent("Audio Podcast RSS FEED");
            myFeed.Links.Add(SyndicationLink.CreateAlternateLink(new Uri("https://versolstore.blob.core.windows.net/"+user.CompanyName.ToLower()+"/audios/rss.xml")));
            myFeed.Links.Add(SyndicationLink.CreateSelfLink(new Uri("https://versolstore.blob.core.windows.net/" + user.CompanyName.ToLower() + "/audios/rss.xml")));
            myFeed.Copyright = TextSyndicationContent.CreatePlaintextContent("All rights reserved");
            myFeed.Language = "en-us";

            //Return the feed's xml content as the response
            MemoryStream ms = new MemoryStream();
            XmlWriter feedWriter = XmlWriter.Create(ms);
            Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(myFeed);
            rssFormatter.WriteTo(feedWriter);
            feedWriter.Close();
            var container = client.GetContainerReference(user.CompanyName.ToLower());
            var blob = container.GetBlockBlobReference("audios/audio.rss");
            blob.UploadFromByteArray(ms.ToArray(), 0, ms.ToArray().Length);
            ms.Close();
        }
        private void createVideoSyndicationFeed()
        {
            //Create syndicated feed
            var myFeed = new SyndicationFeed();
            myFeed.Title = TextSyndicationContent.CreatePlaintextContent(user.CompanyName);
            myFeed.Description = TextSyndicationContent.CreatePlaintextContent("Video Podcast RSS FEED");
            myFeed.Links.Add(SyndicationLink.CreateAlternateLink(new Uri("https://versolstore.blob.core.windows.net/" + user.CompanyName.ToLower()+"devstoreaccount1/videos/rss.xml")));
            myFeed.Links.Add(SyndicationLink.CreateSelfLink(new Uri("https://versolstore.blob.core.windows.net/" + user.CompanyName.ToLower() + "devstoreaccount1/videos/rss.xml")));
            myFeed.Copyright = TextSyndicationContent.CreatePlaintextContent("All rights reserved");
            myFeed.Language = "en-us";

            //Return the feed's xml content as the response
            MemoryStream ms = new MemoryStream();
            XmlWriter feedWriter = XmlWriter.Create(ms);
            Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(myFeed);
            rssFormatter.WriteTo(feedWriter);
            feedWriter.Close();
            var container = client.GetContainerReference(user.CompanyName.ToLower());
            var blob = container.GetBlockBlobReference("videos/video.rss");
            blob.UploadFromByteArray(ms.ToArray(), 0, ms.ToArray().Length);
            ms.Close();
        }
    }
}