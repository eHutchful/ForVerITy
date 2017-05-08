using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DivineApp.Dashboard
{
    public partial class Tables : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AsyncMode = true;
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Sign_in.aspx");
            }
        }
    }
}