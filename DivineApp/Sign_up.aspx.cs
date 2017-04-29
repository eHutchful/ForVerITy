using DivineApp.App_Start;
using DivineApp.Contexts;
using DivineApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
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
            var user = new CompanyUser()
            {
                UserName = u_name.Text,
                Email = email.Text,
                PhoneNumber = phone.Text,
                Address = c_address.Text,
                CompanyName = company.Text,
                FirstName = f_name.Text,
                LastName = l_name.Text,
                Location = location.Text
            };
            //RegisterAsyncTask(new PageAsyncTask(register));
            var context = new MyContext();
            //var userStore = new UserStore<CompanyUser>(context);
           // var manager = new UserManager<CompanyUser>(userStore);
            var manager = Context.GetOwinContext().GetUserManager<CompanyUserManager>();
            
            
            IdentityResult result = manager.Create(user, pass.Text);
            if (result.Succeeded)
            {
                //StatusMessage.Text = string.Format("User {0} was created successfully!", user.UserName);
                //var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                ////download miscrosoft.owin.host.systemweb
                //var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                //authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                //Response.Redirect("~/Sign_in.aspx");
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                manager.SendEmail(user.Id, "Confirm your account",
                    "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                if (user.EmailConfirmed)
                {
                    IdentityHelper.SignIn(manager, user, isPersistent: false);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    StatusMessage.Text = "An email has been sent to your account. Please view the email and confirm your account to complete the registration process.";
                }
            }
            else
            {
                StatusMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}