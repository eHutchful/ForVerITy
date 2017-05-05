using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DivineApp.Dashboard
{
    public partial class AudioUpload : System.Web.UI.Page
    {
        private CloudStorageAccount storageAccount;
        protected void Page_Load(object sender, EventArgs e)
        {
            storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
        }

        protected void a_upload_Click(object sender, EventArgs e)
        {
            if (upload_file.HasFile)
            {
                try
                {
                    
                    var client= storageAccount.CreateCloudBlobClient();
                    var container = client.GetContainerReference("audios");                    
                    var blob = container.GetBlockBlobReference(upload_file.FileName);
                    blob.UploadFromStream(upload_file.FileContent);
                }
                catch { }
            }
        }
    }
}