using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DivineApp
{
    public partial class Sign : System.Web.UI.Page
    {
        public User user;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            RegisterAsyncTask(new PageAsyncTask(authenticate));


        }

        private async Task authenticate()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("Https://divineveritiestrial.azurewebsites.net");
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("username",u_name.Text),
                new KeyValuePair<string, string>("password",pass.Text),
                new KeyValuePair<string, string>("grant_type","password")

            });
            var result = await client.PostAsync("/oauth/token", content);
            string resultContent = await result.Content.ReadAsStringAsync();
            try { user = JsonConvert.DeserializeObject<User>(resultContent); }
            catch(Exception e) { }
            if (user != null)
            {
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

    }
}