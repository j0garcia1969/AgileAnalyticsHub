using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapstoneWebApplication.Subject
{
    public partial class Text : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null) { Response.Redirect("~/Account/Index"); }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            string test_message = Session["test_message"].ToString();

            Label1.Text = test_message;
            Label2.Text = "Refreshed at: " + DateTime.Now.ToLongTimeString();
        }
    }
}