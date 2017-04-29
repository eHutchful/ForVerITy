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
using DivineApp.Contexts;
using DivineApp.Models;
using Microsoft.AspNet.Identity.Owin;
using DivineApp.App_Start;

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
                    Response.Redirect("~/Dashboard.aspx");
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
            #region oldcode
            //var context = new MyContext();
            //var userStore = new UserStore<CompanyUser>(context);
            //var manager = new UserManager<CompanyUser>(userStore);
            //var user = manager.Find(u_name.Text, pass.Text);

            //if (user != null)
            //{
            //    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            //    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

            //    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
            //    Response.Redirect("~/Dashboard.aspx");
            //}
            //else
            //{
            //   // StatusText.Text = "Invalid username or password.";
            //    //LoginStatus.Visible = true;
            //}
            #endregion

            if (IsValid)
            {
                //Validate user password
                var manager = Context.GetOwinContext().GetUserManager<CompanyUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                //Require email confirmation
                var user = manager.FindByName(u_name.Text);
                if (user != null)
                {
                    if (!user.EmailConfirmed)
                    {
                        StatusText.Text = "Invalid login attempt. You must have a confirmed email account.";
                    }
                    else
                    {
                        var result = signinManager.PasswordSignIn(u_name.Text,pass.Text, false, shouldLockout: false);

                        switch (result)
                        {
                            case SignInStatus.Success:
                                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                                Response.Redirect("~/Dashboard.aspx");
                                break;
                            case SignInStatus.LockedOut:
                                Response.Redirect("/Account/Lockout");
                                break;
                            case SignInStatus.RequiresVerification:
                                Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                            Request.QueryString["ReturnUrl"],
                                                            false),
                                              true);
                                break;
                            case SignInStatus.Failure:
                            default:
                                StatusText.Text = "Invalid login attempt";
                                break;
                        }
                    }
                }

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