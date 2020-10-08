using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace AutoTest
{
    public partial class Main : System.Web.UI.Page
    {
        public unib2b b;
        public uniairec ec;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                unib2b b = new unib2b();                
                Session["b"] = b;
                ec = new uniairec();
                Session["ec"] = ec;
            }
        }
         
        protected void Button2_Click(object sender, EventArgs e)
        {
            b = rtnB();
            b.SetupTest();
            b.login();            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            b = rtnB();            
            logs.Text +=b.order3();
        }
        private unib2b rtnB() {
            if (Session["b"] == null)
            {
                unib2b b = new unib2b();
                b.SetupTest();
                b.SetPara(setPara());
                Session["b"] = b;
            }
            else {
                b = (unib2b)(Session["b"]);
                if (b.ifP())
                    b.SetPara(setPara()); 
                Session["b"] = b;
            }
            return (unib2b)(Session["b"]);
        }
        private string[] setPara() {
            string[] temp = { ddl1.SelectedValue, ddl2.SelectedValue, agtArea.Text, agtName.Text, pnum.Text, flyno.Text, gnum.Text, txt_y.Text, txt_m.Text, txt_d.Text, pnr.Text };
            return temp;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            b = rtnB(); 
            logs.Text += b.setTraveller();       
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            b = rtnB();
            b.openConfirm();   
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            ec = rtnEC();
            ec.tst();
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            ec = rtnEC();
            ec.adhoc();
        }

        private uniairec rtnEC()
        {
            if (Session["ec"] == null)
            {
                ec = new uniairec();
                ec.SetupTest();
                ec.SetPara(setPara());
                Session["ec"] = ec;
            }
            else {
                ec = (uniairec)(Session["ec"]);  
                if(ec.ifP())
                    ec.SetPara(setPara());
                Session["ec"] = ec;
            }
            return (uniairec)(Session["ec"]);
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            b = rtnB();
            b.saveTraveller(); 
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            ec = rtnEC();
            ec.SetupTest();
            ec.login();
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            ec = rtnEC();
            ec.addSERIES("allmonth");
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            logs.Text = "開始團體開票smokeTest";
            ec = rtnEC();
            ec.SetupTest();
            ec.SetPara(setPara());
            ec.login();
            logs.Text = "登入EC成功";
            ec.adhoc();
            string _logs = ec.approveADHOC();
            if (_logs.Contains("失敗"))
            {
                logs.Text = "停止測試：" + _logs;
                return;
            }
            else
                logs.Text = "開ad-hoc成功";
            ec.closeWin();
            b = rtnB();
            b.SetupTest();
            b.SetPara(setPara());
            b.login();
            logs.Text = "登入前台成功";
            b.setTraveller();
            logs.Text = "前台名單送審成功";
            b.closeWin();
            ec.SetupTest();
            ec.SetPara(setPara());
            ec.login();
            logs.Text = "登入EC/2成功";
            ec.tst();
            logs.Text = "計價成功";
            ec.closeWin();
            b.SetupTest();
            b.SetPara(setPara());
            b.login();
            logs.Text = "登入前台/2成功";
            b.openConfirm();
            logs.Text = "前台扣款、確認開票成功";
            b.closeWin();
            ec.SetupTest();
            ec.SetPara(setPara());
            ec.login();
            logs.Text = "登入EC/3成功";
            ec.openTKT();
            logs.Text = "後台開票作業成功";
            ec.closeWin();
            logs.Text = "完成團體開票smokeTest";
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            ec = rtnEC();
            logs.Text = ec.approveADHOC();
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            ec = rtnEC();
            ec.openTKT();
        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            ec = rtnEC();
            logs.Text = ec.manuallyTKT() ;
        }
         
        protected void chkAGTkeyword_Click(object sender, EventArgs e)
        {
            ec = rtnEC();
            logs.Text = ec.chkAGTkeyword();
        }

        protected void SERIESbyday_Click(object sender, EventArgs e)
        {
            ec = rtnEC();
            ec.addSERIES("bydays");
            logs.Text = ec.SERIESchart();
            logs.Text = ec.SERIESEdit();
        }

        protected void grpRefund_Click(object sender, EventArgs e)
        {
            ec = rtnEC();
            logs.Text = ec.grpRefund();
        }
         
    }
}