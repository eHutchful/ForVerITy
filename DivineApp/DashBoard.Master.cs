using DivineApp.Models;
using System;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using DivineApp.Contexts;
using Microsoft.AspNet.Identity.Owin;
using DivineApp.App_Start;

namespace DivineApp
{
    public partial class DashBoard : System.Web.UI.MasterPage
    {
        private CompanyUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            string CurrentUserId = HttpContext.Current.User.Identity.GetUserId();
            var manager = Context.GetOwinContext().GetUserManager<CompanyUserManager>();
            user = manager.FindById(CurrentUserId);
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            var autheticationManager = HttpContext.Current.GetOwinContext().Authentication;
            autheticationManager.SignOut();
            Response.Redirect("~/Sign_in.aspx");
        }
    }
}