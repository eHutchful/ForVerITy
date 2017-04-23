using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DivineApp
{
    public partial class Sign_up : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private async Task register()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("Https://divineveritiestrial.azurewebsites.net");
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("Username",u_name.Text),
                new KeyValuePair<string, string>("password",pass.Text),
                new KeyValuePair<string, string>("Email",email.Text),
                new KeyValuePair<string, string>("Level", "3"),
                new KeyValuePair<string, string>("Phone",phone.Text),
                new KeyValuePair<string, string>("FirstName",f_name.Text),
                new KeyValuePair<string, string>("LastName",l_name.Text),
                new KeyValuePair<string, string>("ConfirmPassword",c_pass.Text)
                


            });
            var result = await client.PostAsync("/api/accounts/create", content);
            string resultContent = await result.Content.ReadAsStringAsync();
            dynamic x = JsonConvert.DeserializeObject(resultContent);
            if (x.username != null)
            {
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(register));
        }
    }
}