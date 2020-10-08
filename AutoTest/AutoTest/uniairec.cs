using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace AutoTest
{
    public class uniairec 
    {
        private static string baseURL = "https://uniqaecweb13.uniair.com.tw/uniairec_amadeus4/";
        private static IWebDriver driver;
        private static utility u;
        string logfilename = "";
        private static para p;
        [ClassInitialize()]
        public void SetupTest()
        {
            driver = new InternetExplorerDriver();
            u = new utility(driver);
            driver.Navigate().GoToUrl(baseURL);
            p = new para();
        }
        [TestInitialize()]
        public void SetPara(string[] _para) {
            p.SetPara(_para);
        } 
        [TestMethod]
        public void login()
        { 
            logfilename = "[login]_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            driver.Navigate().GoToUrl(baseURL + "admin/admin_login.aspx");
            Thread.Sleep(100);
            u.setValueonId("txt_USR_ID", "F78539");
            u.setValueonId("txt_USR_PWD", "aul4z83xu06CJ*^"); 
            u.ddlselect("id", "ddl_Domain", "EVAAIR"); 
            driver.FindElement(By.Id("btn_Login")).Click();
        }
        public string chkAGTkeyword() {
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbseradd01.aspx");
            if (!u.rtnifElement("AutoNumber1")) { return "檢查AGT關鍵字進入-SERIES新增，失敗"; }
            if (!opAGTkeyword("AgtNameUC1_kwofAGT", "HotelLocation1_ddl_location", "AgtNameUC1_ddl_AgtName"))
                return "檢查AGT關鍵字進入-SERIES新增，操作失敗";
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbserman01.aspx");
            if (!u.rtnifElement("AutoNumber1")) { return "檢查AGT關鍵字進入-SERIES維護查詢，失敗"; }
            if (!opAGTkeyword("AgtNameUC1_kwofAGT", "HotelLocation1_ddl_location", "AgtNameUC1_ddl_AgtName"))
                return "檢查AGT關鍵字進入-SERIES維護查詢，操作失敗";
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbadhadd01.aspx");
            if (!u.rtnifElement("table2")) { return "檢查AGT關鍵字進入-AD-HOC新增，失敗"; }
            if (!opAGTkeyword("AgtNameUC1_kwofAGT", "HotelLocation1_ddl_location", "AgtNameUC1_ddl_AgtName"))
                return "檢查AGT關鍵字進入-AD-HOC新增，操作失敗";
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbadhman01.aspx");
            if (!u.rtnifElement("table2")) { return "檢查AGT關鍵字進入-AD-HOC維護查詢，失敗"; }
            if (!opAGTkeyword("AgtNameUC1_kwofAGT", "HotelLocation1_ddl_location", "AgtNameUC1_ddl_AgtName"))
                return "檢查AGT關鍵字進入-AD-HOC維護查詢，操作失敗";
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbchkqry01.aspx");
            if (!u.rtnifElement("table2")) { return "檢查AGT關鍵字進入-對團作業(查詢)，失敗"; }
            if (!opAGTkeyword("AgtNameUC1_kwofAGT", "HotelLocation1_ddl_location", "AgtNameUC1_ddl_AgtName"))
                return "檢查AGT關鍵字進入-對團作業(查詢)，操作失敗";
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbvalqry01.aspx");
            if (!u.rtnifElement("table2")) { return "檢查AGT關鍵字進入-名單與計價查詢，失敗"; }
            if (!opAGTkeyword("AgtNameUC1_kwofAGT", "HotelLocation1_ddl_location", "AgtNameUC1_ddl_AgtName"))
                return "檢查AGT關鍵字進入-名單與計價查詢，操作失敗";
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbgrpqry01.aspx");
            if (!u.rtnifElement("table2")) { return "檢查AGT關鍵字進入-團體狀態記錄查詢，失敗"; }
            if (!opAGTkeyword("AgtNameUC1_kwofAGT", "HotelLocation1_ddl_location", "AgtNameUC1_ddl_AgtName"))
                return "檢查AGT關鍵字進入-團體狀態記錄查詢，操作失敗"; 
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gboptqry01.aspx");
            if (!u.rtnifElement("table2")) { return "檢查AGT關鍵字進入-開票作業查詢，失敗"; }
            if (!opAGTkeyword("AgtNameUC1_kwofAGT", "HotelLocation1_ddl_location", "AgtNameUC1_ddl_AgtName"))
                return "檢查AGT關鍵字進入-開票作業查詢，操作失敗";
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbrefqry01.aspx");
            if (!u.rtnifElement("table2")) { return "檢查AGT關鍵字進入-退票作業查詢，失敗"; }
            if (!opAGTkeyword("AgtNameUC1_kwofAGT", "HotelLocation1_ddl_location", "AgtNameUC1_ddl_AgtName"))
                return "檢查AGT關鍵字進入-退票作業查詢，操作失敗";
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbgrpset01.aspx");
            if (!u.rtnifElement("table2")) { return "檢查AGT關鍵字進入-開退票限制/人工開票查詢，失敗"; }
            if (!opAGTkeyword("AgtNameUC1_kwofAGT", "HotelLocation1_ddl_location", "AgtNameUC1_ddl_AgtName"))
                return "檢查AGT關鍵字進入-開退票限制/人工開票查詢，操作失敗";
            driver.Navigate().GoToUrl(baseURL + "admin/report/grpSaleB.aspx");
            if (!u.rtnifElement("Panel1")) { return "檢查AGT關鍵字進入-後端團體銷售報表 ，失敗"; }
            if (!opAGTkeyword("AgtNameUC1_kwofAGT", "HotelLocation1_ddl_location", "AgtNameUC1_ddl_AgtName"))
                return "檢查AGT關鍵字進入-後端團體銷售報表 ，操作失敗";
            return "檢查AGT關鍵字成功完成";
        }
        private bool opAGTkeyword(string keywordCol, string areaDDLid, string agtDDLid)
        { 
            u.setValueonId(keywordCol, "RI");
            u.ddlselect("id", areaDDLid, p.agtArea);
            Thread.Sleep(300);
            if (driver.FindElement(By.Id(agtDDLid)).Text == p.agtName)
            {
                Thread.Sleep(100);
                return true;
            }
            else
                return false;
        }
        public string manuallyTKT() {
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbgrpset01.aspx");
            if (!u.rtnifElement("table2")) { return "人工開票進入搜尋頁失敗"; }
            u.ddlselect("id", "UniCityPair1_ddm_threshold", p.dep);
            Thread.Sleep(300);
            u.ddlselect("id", "UniCityPair1_ddm_destination", p.arr);
            u.setValueonId("UniCalendar2_txtYear", p.dateY);
            u.setValueonId("UniCalendar2_txtMonth", p.dateM);
            u.setValueonId("UniCalendar2_txtDay", p.dateD);
            driver.FindElement(By.Id("Button1")).Click();
            if (!u.rtnifElement("GrpTableListUC1_DataGrid1")) { return "人工開票進入頁面失敗"; }
            for (int i = 1; i < 30; i++)
            {
                if (u.rtnifElementxpath(string.Format("//*[@id='GrpTableListUC1_DataGrid1']/tbody/tr[{0}]/td[3]", i)))
                {
                    if (p.pnr == driver.FindElement(By.XPath(string.Format("//*[@id='GrpTableListUC1_DataGrid1']/tbody/tr[{0}]/td[3]", i))).Text)
                    {
                        driver.FindElement(By.Name(string.Format("GrpTableListUC1$DataGrid1$ctl{0}$ctl00",i.ToString().PadLeft(2,'0')))).Click();
                        if (!u.rtnifElement("AutoNumber1")) { return "人工開票進入PNR失敗"; }
                        driver.FindElement(By.Id("Button1")).Click();
                        var alert_win = driver.SwitchTo().Alert();
                        if (alert_win.Text.Contains("上傳旅客"))
                            alert_win.Accept();
                        return p.pnr + "人工開票成功";
                    }
                    else { continue; }
                }
                else { return "人工開票查無PNR"; }
            }
            return "人工開票未完成";
        }
        public void openTKT() {
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gboptqry01.aspx");
            if (!u.rtnifElement("table2")) { return; }
            u.ddlselect("id", "UniCityPair1_ddm_threshold", p.dep);
            Thread.Sleep(300);
            u.ddlselect("id", "UniCityPair1_ddm_destination", p.arr);
            u.setValueonId("UniCalendar2_txtYear", p.dateY);
            u.setValueonId("UniCalendar2_txtMonth", p.dateM);
            u.setValueonId("UniCalendar2_txtDay", p.dateD);
            driver.FindElement(By.Id("btn_Confirm")).Click();
            if (!u.rtnifElement("DataGrid1_chk_SelectAll_Y")) { return; }
            driver.FindElement(By.Id("DataGrid1_chk_SelectAll_Y")).Click();
            Thread.Sleep(300);
            driver.FindElement(By.Id("btn_OpenTKTs")).Click();
            var alert_win = driver.SwitchTo().Alert();
            if (alert_win.Text.Contains("開票"))
                alert_win.Accept();
            if (u.rtnifElementxpath("//button"))
            {
                driver.FindElement(By.XPath("//button")).Click(); 

            }

        }
        public string grpRefund()
        {
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbrefqry01.aspx");
            if (!u.rtnifElement("table2")) { return "團體退票，進入搜尋頁失敗"; }
            u.ddlselect("id", "HotelLocation1_ddl_location", p.agtArea);
            Thread.Sleep(300);
            u.ddlselect("id", "AgtNameUC1_ddl_AgtName", p.agtName);
            u.ddlselect("id", "UniCityPair1_ddm_threshold", p.dep);
            Thread.Sleep(300);
            u.ddlselect("id", "UniCityPair1_ddm_destination", p.arr);
            u.setValueonId("UniCalendar2_txtYear", p.dateY);
            u.setValueonId("UniCalendar2_txtMonth", p.dateM);
            u.setValueonId("UniCalendar2_txtDay", p.dateD);
            driver.FindElement(By.Id("Button1")).Click();
            if (!u.rtnifElement("GrpTableListUC1_DataGrid1")) { return "團體退票查詢結果異常"; }
            for (int i = 2; i < 30; i++)
            {
                try
                {
                    if (driver.FindElement(By.XPath(string.Format("//*[@id='GrpTableListUC1_DataGrid1']/tbody/tr[{0}]/td[13]", i))).Text == "開票成功") {
                        driver.FindElement(By.Name(string.Format("GrpTableListUC1$DataGrid1$ctl{0}$ctl00",i.ToString().PadLeft(2,'0')))).Click();
                        if (!u.rtnifElement("Table3")) { return "團體退票，進入PNR異常"; }
                        //取得本團grp_nbr
                        string[] grpnbr = driver.FindElement(By.XPath("//*[@id='table1']/tbody/tr[2]/td[1]/span")).GetAttribute("id").Split('_');//GrpTraInfo_306725_Lab_GrpNo

                        if (driver.FindElement(By.XPath("//*[@id='GrpTraInfo_" + grpnbr[1] + "_DataGrid1']/tbody/tr[2]/td[10]")).Text == "已退票")
                        {
                            driver.FindElement(By.Id("Button1")).Click();
                            continue;
                        }
                        driver.FindElement(By.Id("allcheck")).Click();
                        Thread.Sleep(300);
                        driver.FindElement(By.Id("GrpTraInfo_" + grpnbr[1] + "_Button1")).Click();//GrpTraInfo_306725_Button1
                        Thread.Sleep(300);
                        var alert_win = driver.SwitchTo().Alert();
                        if (alert_win.Text.Contains("確認退票"))
                            alert_win.Accept();
                        Thread.Sleep(5000);
                        if (!u.rtnifElement("LSuss")) { return "團體退票時有異常"; }
                        return "團體退票成功完成";
                    }
                    else
                        continue;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return "團體退票不明異常";
        }
        public string SERIESchart()
        {
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbserpre01.aspx");
            if (!u.rtnifElement("AutoNumber1")) { return "SERIES預覽表，進入搜尋頁失敗"; }
            u.ddlselect("id", "UniCityPair1_ddm_threshold", p.dep);
            Thread.Sleep(300);
            u.ddlselect("id", "UniCityPair1_ddm_destination", p.arr);
            u.ddlselect("id", "ddm_yearmonth", p.dateY + "/" + p.dateM);
            driver.FindElement(By.Id("Button1")).Click();
            if (!u.rtnifElement("Label1")) { return "SERIES預覽表，查詢失敗"; }
            for (int i = 1; i < 20; i++)
            {
                try
                {
                    if (driver.FindElement(By.XPath(string.Format("//*[@id='ID']/table/tbody/tr[2]/td[2]/p/table/tbody/tr[{0}]/td[1]", i))).Text == p.flyno + "/")
                    {
                        driver.FindElement(By.Id("Button2")).Click();
                        if (!u.rtnifElement("AutoNumber1")) { return "SERIES預覽表，進入搜尋頁/2失敗"; }
                        return "SERIES預覽表查詢成功";
                    }
                    else
                        continue;
                }
                catch (Exception)
                {
                        return "SERIES預覽表，查詢班次失敗";
                     
                }
            }
            return "SERIES預覽表，查詢班次異常";
        }
        public string approveADHOC() {
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbadhapp01.aspx");
            if (!u.rtnifElement("UniCityPair1_ddm_threshold")) { return "ad-hoc進入搜尋頁失敗";  }
            u.ddlselect("id", "UniCityPair1_ddm_threshold", p.dep);
            Thread.Sleep(300);
            u.ddlselect("id", "UniCityPair1_ddm_destination", p.arr);
            driver.FindElement(By.Id("Button1")).Click();
            if (!u.rtnifElement("DataGrid1")) { return "ad-hoc查無資料"; }
            driver.FindElement(By.Id("ButChked")).Click();
            Thread.Sleep(300);
            driver.FindElement(By.Id("Button1")).Click();
            var alert_win = driver.SwitchTo().Alert();
            if (alert_win.Text.Contains("審核"))
                alert_win.Accept();
            int chk = 0;
            do{
                Thread.Sleep(1000);
                try
                {
                        if (driver.FindElement(By.Id("Lab_CreateArrive")).Text == "開團成功")
                            return "ad-hoc審核成功";
                        else
                            return "ad-hoc審核失敗";
                     
                }
                catch (Exception)
                {
                    if (u.rtnifElement("Lab_CreateBackfire")) { return "ad-hoc審核回覆為:失敗"; }
                    if (chk > 10) {
                        return "ad-hoc審核，超時失敗";
                    }
                }      
                 chk++;          
            }while(chk<10);
            return "失敗";
        }
        public void addSERIES(string op) {
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbseradd01.aspx");
            if (!u.rtnifElement("AutoNumber1")) { return; }
            u.ddlselect("id", "HotelLocation1_ddl_location", p.agtArea);
            Thread.Sleep(300);
            u.ddlselect("id", "AgtNameUC1_ddl_AgtName", p.agtName);
            u.ddlselect("id", "UniCityPair1_ddm_threshold", p.dep);
            Thread.Sleep(500);
            u.ddlselect("id", "ddm_yearmonth", p.dateY + "/" + p.dateM);
            Thread.Sleep(300);
            u.ddlselect("id", "UniCityPair1_ddm_destination", p.arr);
            u.setValueonId("FlyNum_F", p.flyno);
            u.ddlselect("id", "PatternUC1_ddl_Pattern", "單程");
            u.setValueonId("txt_sitnum", p.pnum);
            if (op == "allmonth")
            {
                driver.FindElement(By.Id("Button3")).Click();
                Thread.Sleep(300);
            }
            else if (op == "bydays") {
                u.setValueonId( "dayvalue", p.pnum);
            }
            driver.FindElement(By.Id("Button1")).Click();
            var alert_win = driver.SwitchTo().Alert();
            if (alert_win.Text.Contains("新增"))
                alert_win.Accept();
            if (u.rtnifElementxpath("//button"))
            {
                driver.FindElement(By.XPath("//button")).Click();             
              
            }
        }
        public string SERIESEdit()
        {
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbserman01.aspx");
            if (!u.rtnifElement("AutoNumber1")) { return "SERIES維護，進入搜尋頁面失敗"; }
            u.ddlselect("id", "UniCityPair1_ddm_threshold", p.dep);
            Thread.Sleep(300);
            u.ddlselect("id", "UniCityPair1_ddm_destination", p.arr);
            u.ddlselect("id", "ddm_yearmonth", p.dateY + "/" + p.dateM);
            driver.FindElement(By.Id("Button1")).Click();
            if (!u.rtnifElement("DataGrid1")) { return "SERIES維護失敗"; }
            for (int i = 2; i < 30; i++)
            {
                if (u.rtnifElementxpath(string.Format("//*[@id='DataGrid1']/tbody/tr[{0}]/td[7]", i)))
                {
                    if ("B7 "+p.flyno == driver.FindElement(By.XPath(string.Format("//*[@id='DataGrid1']/tbody/tr[{0}]/td[6]", i))).Text)
                    {
                        driver.FindElement(By.Name(string.Format("DataGrid1$ctl{0}$ctl00", i.ToString().PadLeft(2, '0')))).Click();
                        if (!u.rtnifElement("AutoNumber1")) { return "SERIES維護進入維護頁失敗"; }
                        if (driver.FindElement(By.Id("dayvalue")).GetAttribute("value") == p.pnum)
                        {
                            u.setValueonId("dayvalue", "7");
                            driver.FindElement(By.Id("Button2")).Click();
                            var alert_win = driver.SwitchTo().Alert();
                            if (alert_win.Text.Contains("確認修改"))
                                alert_win.Accept();
                            if (u.rtnifElementxpath("//button"))
                            {
                                driver.FindElement(By.XPath("//button")).Click();
                                if (u.rtnifElementxpath(string.Format("//*[@id='DataGrid1']/tbody/tr[{0}]/td[7]", i)))
                                {
                                    driver.FindElement(By.Name(string.Format("DataGrid1$ctl{0}$ctl00", i.ToString().PadLeft(2, '0')))).Click();
                                    if (!u.rtnifElement("AutoNumber1")) { return "SERIES維護進入維護頁/2失敗"; }
                                    if (driver.FindElement(By.Id("dayvalue")).GetAttribute("value") != "7") {
                                        return "SERIES維護修改，未儲存成功";
                                    }
                                    driver.FindElement(By.Id("Button3")).Click();
                                    alert_win = driver.SwitchTo().Alert();
                                    if (alert_win.Text.Contains("確認刪除"))
                                        alert_win.Accept();
                                    if (u.rtnifElementxpath("//button"))
                                    {
                                        driver.FindElement(By.XPath("//button")).Click();
                                        return "SERIES維護成功";
                                    }
                                    else
                                        return "SERIES維護刪除失敗";
                                }
                            }else
                                return "SERIES維護修改失敗";
                        }
                        else
                            return "SERIES維護，人數異常";
                    }
                    else { continue; }
                }
                else { return "人工開票查無PNR"; }
            }
            return "SERIES維護，未成功";
        }
        public void tst() {
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbvalqry01.aspx");
            Thread.Sleep(300);
            u.ddlselect("id", "HotelLocation1_ddl_location", p.agtArea);
            Thread.Sleep(500);
            u.ddlselect("id", "AgtNameUC1_ddl_AgtName", p.agtName);
            driver.FindElement(By.Id("Button1")).Click();
            if (!u.rtnifElement("GrpTableListUC1_DataGrid1"))
                return;
            Boolean chkEle=false;
            //進入列表頁
            for (int i = 1; i < 30; i++)
            {
                try
                {
                    var dv = driver.FindElement(By.XPath(string.Format("//*[@id='GrpTableListUC1_DataGrid1']/tbody/tr[{0}]/td[13]", i)));
                    if (dv.Text == "已送審待計價")
                    {
                        chkEle = true;
                        driver.FindElement(By.Name(string.Format("GrpTableListUC1$DataGrid1$ctl{0}$ctl00", i.ToString().PadLeft(2, '0')))).Click();
                        i = i - 1;
                        if (u.rtnifElement("ctl00_DataGrid1"))
                        {
                            driver.FindElement(By.Id("Button1")).Click();
                            if (u.rtnifElement("table1"))
                            {
                                driver.FindElement(By.Id("Button1")).Click();
                                if (u.rtnifElement("ctl00_DataGrid1"))
                                {
                                    driver.FindElement(By.Id("Button1")).Click();
                                    if (u.rtnifElementxpath("//button"))
                                    {
                                        driver.FindElement(By.XPath("//button")).Click();
                                        if (u.rtnifElement("GrpTableListUC1_DataGrid1"))
                                            continue;
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    if (u.rtnifElementxpath(string.Format("//*[@id='GrpTableListUC1_DataGrid1']/tbody/tr[{0}]/td[13]", i)))
                    {
                    }
                    else
                    {
                        break;
                    }
                } 
            }

        }
        public void adhoc()
        {
            driver.Navigate().GoToUrl(baseURL + "admin/grp/gbadhadd01.aspx");
            Boolean chkEle = true;
            string[] _pickOrderDate = u.getOrderDay(2).Split('/');
            int i = 2;
            int doagain = Int32.Parse(p.gnum);
            while (doagain > 0) { 
                do
                {
                    Thread.Sleep(2000);
                    try
                    {
                        if (driver.FindElement(By.Id("table2")) != null)
                        {
                            chkEle = false;
                        }
                    }
                    catch (Exception)
                    { 
                    }
                } while (chkEle);
                u.ddlselect("id", "HotelLocation1_ddl_location", p.agtArea);
                Thread.Sleep(500);
                u.ddlselect("id", "AgtNameUC1_ddl_AgtName", p.agtName);
                _pickOrderDate = u.getOrderDay(i).Split('/');
                u.setValueonId("UniCalendar1_txtYear",_pickOrderDate[0]);
                u.setValueonId("UniCalendar1_txtMonth", _pickOrderDate[1]);
                u.setValueonId("UniCalendar1_txtDay", _pickOrderDate[2]);
                u.ddlselect("id", "UniCityPair1_ddm_threshold", p.dep);
                Thread.Sleep(100);
                u.ddlselect("id", "UniCityPair1_ddm_destination", p.arr);
                u.setValueonId("TextBox_flightNo1", p.flyno);
                u.setValueonId("TextBox_countNo", p.pnum);
                driver.FindElement(By.Id("Button1")).Click();
                Thread.Sleep(1000);
                var alert_win = driver.SwitchTo().Alert();
                if (alert_win.Text.Contains("新增"))
                    alert_win.Accept();
                doagain = doagain - 1;
                i = i + 1;
                if (u.rtnifElementxpath("//button")) {
                    driver.FindElement(By.XPath("//button[2]")).Click();
                }
            }
        }
        public void closeWin() {
            driver.Close();
        }
        public bool ifP() {
            return p != null;
        }
    }
}