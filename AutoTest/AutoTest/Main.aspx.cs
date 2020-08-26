using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoTest
{
    public partial class Main : System.Web.UI.Page
    {
        public unib2b b;
        protected void Page_Load(object sender, EventArgs e)
        {
            unib2b b = new unib2b();
            Session["b"] = b;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            b = (unib2b)(Session["b"]);
            b.SetupTest();
            b.login();            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            b = (unib2b)(Session["b"]);
            if (b.chklogin()) {
                log.Text += "登入成功";
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            b = (unib2b)(Session["b"]);
            b.order1();
        }
    }
}