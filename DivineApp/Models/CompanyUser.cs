using DivineApp.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace DivineApp.Models
{
    public class CompanyUser:IdentityUser
    {
        public CompanyUser() : base() { }
        public CompanyUser(string username) : base(username)
        {
            
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string CompanyName { get; set; }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(CompanyUserManager manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }
        public ClaimsIdentity GenerateUserIdentity(CompanyUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}