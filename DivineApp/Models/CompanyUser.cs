using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}