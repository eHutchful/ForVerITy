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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace DivineApp
{
    public partial class Sign_in : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    //what to do when authenticated
                }
                else
                {
                    //what to do when not authenticated
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //RegisterAsyncTask(new PageAsyncTask(authenticate));

            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(u_name.Text, pass.Text);

            if (user != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                Response.Redirect("~/Dashboard.aspx");
            }
            else
            {
               // StatusText.Text = "Invalid username or password.";
                //LoginStatus.Visible = true;
            }
        }

        //private async Task authenticate()
        //{
        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri("Https://divineveritiestrial.azurewebsites.net");
        //    var content = new FormUrlEncodedContent(new[]
        //    {
        //        new KeyValuePair<string,string>("username",u_name.Text),
        //        new KeyValuePair<string, string>("password",pass.Text),
        //        new KeyValuePair<string, string>("grant_type","password")

        //    });
        //    var result = await client.PostAsync("/oauth/token", content);
        //    string resultContent = await result.Content.ReadAsStringAsync();
        //    try { user = JsonConvert.DeserializeObject<User>(resultContent); }
        //    catch(Exception e) { }
        //    if (user != null)
        //    {
        //        Response.Redirect("Dashboard.aspx");
        //    }
        //    else
        //    {
        //        Response.Redirect("Default.aspx");
        //    }
        //}
        //protected void SignOut(object sender, EventArgs e)
        //{
        //    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
        //    authenticationManager.SignOut();
        //    Response.Redirect("~/Login.aspx");
        //}
    }
}