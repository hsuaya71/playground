using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;

namespace F78539
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string pS_SqlStr = "";
        //連線字串 
        string pS_ConnStr = "Provider=MSDAORA;Data Source=testinet.evaair.com ;Password=tebms1019;User ID=ebms";

        public DataTable a;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                a = setData();
                    GridView1.DataSource = a;
                    GridView1.DataBind();
                CheckBrowserCaps();
            }
        }

        private void CheckBrowserCaps()
        {
            System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
            Label1.Text = ((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).Type.ToUpper();
            Label2.Text = Request.UserAgent;

            string sIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(sIP))
                sIP = Request.ServerVariables["REMOTE_ADDR"];
            else
                sIP = sIP.Split(new char[] { ',' })[0];
            Label3.Text = sIP;
            Label4.Text = ((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice.ToString();
        }

        protected void dogroup(object sender, EventArgs e)
        {
            fun2();
        }
        private void fun2() {
            DataTable dt_source = setData();
            DataTable dt_grouped = dt_source.DefaultView.ToTable("Grouped",
                                                       true,
                                                       new[] { "num" });
            dt_grouped.Columns.Add("sum", typeof(double));
            foreach (DataRow dr in dt_grouped.Rows)
            {
                dr["sum"] = dt_source.Compute("SUM(price)", String.Format("num='{0}'", dr["num"])); //轉型有問題
                dt_grouped.AcceptChanges();
            }

            GridView2.DataSource = dt_grouped;
            GridView2.DataBind();
        }
        private void fun1(){
            DataTable aa = setData();
            var products = aa.AsEnumerable();
            var query = from product in products
                        group product by product.Field<string>("num") into g
                        select new
                        {
                            Num = g.Key,
                            Sum =
                                g.Sum(product => Convert.ToDecimal(product.Field<String>("price")))
                        };
            DataTable newData = new DataTable();
            DataRow r;
            newData.Columns.Add("num");
            newData.Columns.Add("sum");
            foreach (var product in query)
            {
                r = newData.NewRow();
                r["num"] = product.Num;
                r["sum"] = product.Sum;
                newData.Rows.Add(r);
            }

            GridView2.DataSource = newData;
            GridView2.DataBind();

        }
        private DataTable setData() {
            DataTable ds = new DataTable();
            DataRow row; 
            ds.Columns.Add("num");
            ds.Columns.Add("price");
            for (var i = 0; i < 5; i++) {
                row = ds.NewRow();
                row["num"] = "A";
                row["price"] = (i + 1) * 10;
                ds.Rows.Add(row);
            }
            for (var i = 0; i < 3; i++)
            {
                row = ds.NewRow();
                row["num"] = "B";
                row["price"] = (i + 3) * 4;
                ds.Rows.Add(row);
            }
            for (var i = 0; i < 4; i++)
            {
                row = ds.NewRow();
                row["num"] = "C";
                row["price"] = (i + 12) * 3;
                ds.Rows.Add(row);
            }
            return ds;
        }
        
    }
}