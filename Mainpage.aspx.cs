using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SeleniumTests;

namespace F78539
{
    public partial class Mainpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!Page.IsPostBack)
            {

            }
        }

        protected void RUN_Click(object sender, EventArgs e)
        {
            //使用IE
            EVALogin b = new SeleniumTests.EVALogin();
            b.SetupTest();
            string rsl = b.TheEva_ebms_05();
            result_msg.Text += rsl;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            result_msg.Text = "";
        }
         
         
    }
}