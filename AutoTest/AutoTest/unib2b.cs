using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
    public class unib2b
    {
        private static string baseURL = "https://uniqaecweb13.uniair.com.tw";
        private static IWebDriver driver;
        private static utility u ;
        string logfilename = "";
        private static para p;
        [ClassInitialize()]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            u = new utility(driver);
            driver.Navigate().GoToUrl(baseURL+"/uniagent/bmem_login.aspx");
            p = new para();
        } 
        [TestMethod]
        public void login() {
            driver.Navigate().GoToUrl(baseURL + "/uniagent/bmem_login.aspx");            
            logfilename = "[login]_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            u.sub_WriteTxt("UNIB2B登入", "bmem_login.aspx", logfilename);

            Thread.Sleep(1000);
            u.textinput("id", "txtAgentUniNo", "11223344");
            u.textinput("id", "txtUserId", "ayahsu");
            u.textinput("id", "txtPassword", "89562356a");
            if (!u.rtnifElement("ui_t01")) { return; }
        }
        public void SetPara(string[] _para)
        {
            p.SetPara(_para);
        }
        public Boolean chklogin() {
            if (driver.FindElement(By.Id("ui_t01")).Text == "最新公告")
            {
                return true;
            }
            else
                return false;
        }
        public bool chkVcode(string vcode) {
            if (vcode.Length == 6)
            { 
                driver.FindElement(By.Id("btnLogin")).Click();
                return true;
            }
            return false;
        }
        public void openConfirm() {
            driver.Navigate().GoToUrl(baseURL + "/uniagent/grp/gfoptqry01.aspx");
            u.ddlselect("id", "SelectGrpUCF1_UniCityPair1_ddm_threshold", p.dep);
            u.ddlselect("id", "SelectGrpUCF1_UniCityPair1_ddm_destination", p.arr);
            u.setValueonId("SelectGrpUCF1_UniCalendar2_txtDay", "30");
            driver.FindElement(By.Id("Button1")).Click();
            Boolean chkEle = true; 
            if (!u.rtnifElement("GrpTableListUC1_DataGrid1")) { return; }
            //在清單頁選
            int doagain = 30;
            while (doagain > 0)
            {
                try
                {
                    driver.FindElement(By.Name("GrpTableListUC1$DataGrid1$ctl02$ctl00")).Click();
                    //輸入旅客頁
                    if (!u.rtnifElement("ctl00_DataGrid1")) { return; }
                    driver.FindElement(By.Id("Button1")).Click();
                    var alert_win = driver.SwitchTo().Alert();
                    if (alert_win.Text.Contains("開票"))
                        alert_win.Accept();
                    if (!u.rtnifElement("Table1")) { return; }
                    driver.FindElement(By.Id("Button1")).Click();
                    Thread.Sleep(300);

                }
                catch (Exception)
                { 
                    if (u.rtnifElementxpath("//button"))
                    {
                        driver.FindElement(By.XPath("//button")).Click();
                        return;
                    }              
                }
                doagain = doagain - 1;                
            }
        }
        public void saveTraveller() {  
            driver.Navigate().GoToUrl(baseURL + "/uniagent/grp/gflisman01.aspx");
            u.ddlselect("id", "SelectGrpUCF1_UniCityPair1_ddm_threshold", p.dep);
            Thread.Sleep(100);
            u.ddlselect("id", "SelectGrpUCF1_UniCityPair1_ddm_destination", p.arr);
            u.setValueonId("SelectGrpUCF1_UniCalendar2_txtYear", p.dateY);
            u.setValueonId("SelectGrpUCF1_UniCalendar2_txtMonth", p.dateM);
            u.setValueonId("SelectGrpUCF1_UniCalendar2_txtDay", p.dateD);
            driver.FindElement(By.Id("Button1")).Click();
            Boolean chkEle = true;
            if (!u.rtnifElement("GrpTableListUC1_DataGrid1"))
                return ;            
            //在清單頁選
            int doagain = 2;
            while (doagain<30)
            {
                driver.FindElement(By.Name("GrpTableListUC1$DataGrid1$ctl"+doagain.ToString().PadLeft(2,'0')+"$ctl00")).Click();
                //輸入旅客頁
                chkEle = true;
                if (!u.rtnifElement("ctl06_DataGrid1"))
                    continue;
                for (int i = 0; i < Int32.Parse(p.pnum); i++)
                {
                    //ctl06_DataGrid1_TextBox_lname_0
                    u.textinput("id", string.Format("ctl06_DataGrid1_TextBox_lname_{0}", i), u.RandomString(3));
                    u.textinput("id", string.Format("ctl06_DataGrid1_TextBox_fname_{0}", i), u.RandomString(5));
                    u.ddlselect("id", string.Format("ctl06_DataGrid1_GrpPeopleNationUC1_{0}_ddl_Nation_{0}", i), "中國(CN)");
                    u.setValueonId(string.Format("ctl06_DataGrid1_UniCalendarIn1_{0}_txtYear_{0}", i), "1980");
                    u.setValueonId(string.Format("ctl06_DataGrid1_UniCalendarIn1_{0}_txtMonth_{0}", i), "8");
                    u.setValueonId(string.Format("ctl06_DataGrid1_UniCalendarIn1_{0}_txtDay_{0}", i), "1");
                }
                u.ddlselect("id", "ctl07_ddl_PhoneNationCode", "台灣(886)");
                u.textinput("id", "ctl07_txt_Phone", "988555444");
                driver.FindElement(By.Id("SaveListAll")).Click();
                var alert_win = driver.SwitchTo().Alert();
                if (alert_win.Text.Contains("儲存"))
                    alert_win.Accept();
                if (u.rtnifElement("ctl06_DataGrid1")) {               
                driver.FindElement(By.Id("back")).Click();                
                chkEle = true;
                if (!u.rtnifElement("GrpTableListUC1_DataGrid1"))
                    continue;
                } 
                doagain ++;
            }
        }
        public string setTraveller() {
            string msg = "填寫團體旅客資料";
            driver.Navigate().GoToUrl(baseURL + "/uniagent/grp/gflisman01.aspx");
            u.ddlselect("id", "SelectGrpUCF1_UniCityPair1_ddm_threshold", p.dep);
            Thread.Sleep(100);
            u.ddlselect("id", "SelectGrpUCF1_UniCityPair1_ddm_destination", p.arr);
            u.setValueonId("SelectGrpUCF1_UniCalendar2_txtYear", p.dateY);
            u.setValueonId("SelectGrpUCF1_UniCalendar2_txtMonth", p.dateM);
            u.setValueonId("SelectGrpUCF1_UniCalendar2_txtDay", p.dateD);
            driver.FindElement(By.Id("Button1")).Click();
            Boolean chkEle = true;
            if (!u.rtnifElement("GrpTableListUC1_DataGrid1"))
                return "";            
            //在清單頁選
            int doagain = 10;
            while (doagain>0)
            {
                driver.FindElement(By.Name("GrpTableListUC1$DataGrid1$ctl02$ctl00")).Click();
                //輸入旅客頁
                chkEle = true;
                do
                {
                    Thread.Sleep(500);
                    if (driver.FindElement(By.Id("ctl06_DataGrid1")) != null)
                    {
                        chkEle = false;
                    }
                } while (chkEle);
                for (int i = 0; i < Int32.Parse(p.pnum); i++)
                {
                    //ctl06_DataGrid1_TextBox_lname_0
                    u.textinput("id", string.Format("ctl06_DataGrid1_TextBox_lname_{0}", i), u.RandomString(3));
                    u.textinput("id", string.Format("ctl06_DataGrid1_TextBox_fname_{0}", i), u.RandomString(5));
                    u.ddlselect("id", string.Format("ctl06_DataGrid1_GrpPeopleNationUC1_{0}_ddl_Nation_{0}", i), "中國(CN)");
                    u.setValueonId(string.Format("ctl06_DataGrid1_UniCalendarIn1_{0}_txtYear_{0}", i), "1980");
                    u.setValueonId(string.Format("ctl06_DataGrid1_UniCalendarIn1_{0}_txtMonth_{0}", i), "8");
                    u.setValueonId(string.Format("ctl06_DataGrid1_UniCalendarIn1_{0}_txtDay_{0}", i), "1");
                }
                u.ddlselect("id", "ctl07_ddl_PhoneNationCode", "台灣(886)");
                u.textinput("id", "ctl07_txt_Phone", "988555444");
                driver.FindElement(By.Id("Button1")).Click();
                var alert_win = driver.SwitchTo().Alert();
                if (alert_win.Text.Contains("送審"))
                    alert_win.Accept();
                chkEle = true;
                do
                {
                    Thread.Sleep(500);
                    if (driver.FindElement(By.Id("lblBrief")) != null)
                    {
                        chkEle = false;
                    }
                } while (chkEle);
                driver.FindElement(By.XPath("//*[@id='Table1']/tbody/tr/td[2]/button")).Click();                
                chkEle = true;
                do
                {
                    Thread.Sleep(500);
                    try
                    {
                        if (driver.FindElement(By.Name("GrpTableListUC1$DataGrid1$ctl02$ctl00")) != null)
                        {
                            chkEle = false;
                        }
                    }
                    catch (Exception)
                    {
                        if (driver.FindElement(By.XPath("//*[@id='GrpTableListUC1_DataGrid1']/caption")).Text == "查無資料")
                        {
                            chkEle = false;
                            doagain = -1;
                        }
                    }
                } while (chkEle);
                doagain = doagain - 1;
            }
            return msg;
        }
        public string order3() { 
            //主要驗證旅客資料輸入頁
            string msg = "一條龍訂位，旅客資料驗證>";
            driver.Navigate().GoToUrl(baseURL + "/uniagent/dragon/FuncMenu_Dragon_F.aspx"); 
            driver.FindElement(By.LinkText("訂位新增")).Click();
            u.ddlselect("id", "DropDownList1", "松山-金門-廈門五通");
            string[] _pickOrderDate = u.getOrderDay(2).Split('/');
            u.setValueonId("YearSt", _pickOrderDate[0]);
            u.setValueonId("MonthSt", _pickOrderDate[1]);
            u.setValueonId("DaySt", _pickOrderDate[2]);
            u.ddlselect("name", "ddm_adultNum", "1");
            u.ddlselect("name", "ddm_childNum", "1");
            u.ddlselect("name", "ddm_oldNum", "1");  
            driver.FindElement(By.Id("Button2")).Click();
            //進入航班選擇頁
            msg += "航班選擇>";
            Boolean chkEle = true;
            do
            {
                Thread.Sleep(2000);
                if( driver.FindElement(By.Id("Dragonlist1_ShowTBL"))!=null) {
                    chkEle = false;
                }
            } while (chkEle);
            driver.FindElement(By.Id("Button1")).Click();
            //輸入旅客資料
            msg += "輸入旅客資料>";
            driver.FindElement(By.Id("Button3")).Click(); 
            var alert_win = driver.SwitchTo().Alert();
            if (alert_win.Text.Contains("第1位旅客姓氏必填"))
                msg += "檢查姓氏必填/";
            else
                msg += "檢查姓氏必填fail/";
            if (alert_win.Text.Contains("第1位旅客名必填"))
                msg += "檢查名必填/";
            else
                msg += "檢查名必填fail/";
            alert_win.Accept();
            u.textinput("id", "txt_famName_1", "T");
            driver.FindElement(By.Id("Button3")).Click(); 
            alert_win = driver.SwitchTo().Alert();
            if (alert_win.Text.Contains("第1位旅客的姓氏不可為１個英文字母"))
                msg += "檢查姓氏只有1個英文/";
            else
                msg += "檢查姓氏只有1個英文fail/";
            alert_win.Accept();
            return msg;
        }
        public string order1() {
            string msg = "一條龍訂位>";
            driver.Navigate().GoToUrl(baseURL + "/uniagent/dragon/FuncMenu_Dragon_F.aspx"); 
            driver.FindElement(By.LinkText("訂位新增")).Click();
            u.ddlselect("id", "DropDownList1", "松山-金門-廈門五通");
            string[] _pickOrderDate = u.getOrderDay(2).Split('/');
            u.setValueonId("YearSt", _pickOrderDate[0]);
            u.setValueonId("MonthSt", _pickOrderDate[1]);
            u.setValueonId("DaySt", _pickOrderDate[2]);
            u.ddlselect("name", "ddm_adultNum", "1"); 
            //IWebElement submit = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#Button2')[0].click();");
            driver.FindElement(By.Id("Button2")).Click();
            //進入航班選擇頁
            msg += "航班選擇>";
            Boolean chkEle = true;
            do
            {
                Thread.Sleep(2000);
                if( driver.FindElement(By.Id("Dragonlist1_ShowTBL"))!=null) {
                    chkEle = false;
                }
            } while (chkEle);
            driver.FindElement(By.Id("Button1")).Click();
            //輸入旅客資料
            msg += "輸入旅客資料>";
            u.textinput("id", "txt_famName_1", "Test");
            u.textinput("id", "txt_gvnName_1", "LIN");
            _pickOrderDate = u.getOrderDay(-4381).Split('/');
            u.setValueonId("ddm_year_1", _pickOrderDate[0]);
            u.setValueonId("ddm_month_1", _pickOrderDate[1]);
            u.setValueonId("ddm_day_1", _pickOrderDate[2]);
            u.textinput("id", "txt_CupNum_1", "5252115016933");
            u.textinput("id", "txt_idNum1_1", "U156212847");
            driver.FindElement(By.Id("chkbox_VisaOnArrival_1")).Click();
            u.ddlselect("id", "cellCntryNumT", "台灣 (886) ");
            u.textinput("id", "cellnumT", "0911222333");
            driver.FindElement(By.Id("Button3")).Click();
            chkEle = true;
            do
            {
                Thread.Sleep(2000);
                if (driver.FindElement(By.Id("UserList1_ShowTBL")) != null)
                {
                    chkEle = false;
                }
            } while (chkEle);
            msg += "確認旅客資料>";
            if (driver.FindElement(By.XPath("//*[@id='UserList1_ShowTBL']/tbody/tr[5]/td[2]")).Text == "5252115016933")
            {
                driver.FindElement(By.Id("Button3")).Click();
                chkEle = true;
                do
                {
                    Thread.Sleep(2000);
                    if (driver.FindElement(By.Id("FlyList1_ShowTBL")) != null)
                    {
                        chkEle = false;
                    }
                } while (chkEle);
                msg += "訂位成功: " + driver.FindElement(By.XPath("//*[@id='FlyList1_ShowTBL']/tbody/tr[2]/td[2]/font")).Text;
            }
            else
                msg += "未成功!";
            return msg;
        }
        public void closeWin()
        {
            driver.Close();
        }
        public bool ifP()
        {
            return p != null;
        }
        [ClassCleanup()]
        public static void TestCleanup()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
    class para
    {
        public string dep { get; set; }
        public string arr { get; set; }
        public string agtArea { get; set; }
        public string agtName { get; set; }
        public string pnum { get; set; }
        public string flyno { get; set; }
        public string gnum  { get; set; }
        public string dateY  { get; set; }
        public string dateM  { get; set; }
        public string dateD { get; set; } 
        public string pnr { get; set; } 
        public para() {
            dep = "台中(RMQ)";
            arr = "金門(KNH)";
            agtArea = "台東(TTT)";
            agtName = "RITA";
            pnum = "32";
            flyno = "8961";
            gnum = "5";
            dateY = "2020";
            dateM = "10";
            dateD = "1";
            pnr = "";
        }
        public void SetPara(string[] para)
        {
            dep = para[0];
            arr= para[1];
            agtArea = para[2];
            agtName =para[3];
            pnum = para[4];
            flyno=para[5];
            gnum=para[6];
            dateY=para[7];
            dateM=para[8];
            dateD=para[9];
            pnr = para[10];
        } 

    }
    class utility {
        private static IWebDriver driver;
        public utility(IWebDriver _d)
        {
            driver = _d;
        }
        public void setValueonId(string id, string value)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementById('" + id + "').setAttribute('value','" + value + "');");
        }
        public string getOrderDay(int addDays)
        {
            DateTime orderday;
            orderday = DateTime.Today.AddDays(addDays);
            return orderday.ToString("d");
        }
        public void ddlselect(string opt, string key, string value)
        {
            if (opt == "id")
            {
                IWebElement dropDownListBox = driver.FindElement(By.Id(key));
                SelectElement clickThis = new SelectElement(dropDownListBox);
                clickThis.SelectByText(value);
            }
            else if (opt == "name")
            {
                IWebElement dropDownListBox = driver.FindElement(By.Name(key));
                SelectElement clickThis = new SelectElement(dropDownListBox);
                clickThis.SelectByText(value);
            }
        }
        public void textinput(string opt, string key, string value)
        {
            if (opt == "id")
            {
                driver.FindElement(By.Id(key)).Clear();
                driver.FindElement(By.Id(key)).SendKeys(value);
            }
            else if (opt == "name")
            {
                driver.FindElement(By.Name(key)).Clear();
                driver.FindElement(By.Name(key)).SendKeys(value);
            }
        }
        public void sub_WriteTxt(string pS_String, string pS_method, string filename)
        {
            string pS_FileName;
            string pS_Log_Time = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string pS_LogPath = "D:/temp/F78539/Log/";
            string pS_YYYYMMDD = DateTime.Now.Date.ToString("yyyyMMdd");
            pS_LogPath = pS_LogPath + pS_YYYYMMDD + "\\";
            pS_FileName = pS_LogPath + filename;

            if (System.IO.Directory.Exists(pS_LogPath) == false)
            {
                System.IO.Directory.CreateDirectory(pS_LogPath);
            }
            if (System.IO.File.Exists(pS_FileName) == false)
            {
                System.IO.FileStream ss = new System.IO.FileStream(pS_FileName, System.IO.FileMode.Create);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(ss);
                sw.WriteLine(pS_String);
                sw.Close();
            }
            else
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(pS_FileName, true);
                sw.WriteLine(pS_String);
                sw.Close();
            }
        }
        public Boolean rtnifElement(string id) {
            Boolean chkEle = true;
            int trycount = 0;
            do
            {
                Thread.Sleep(500);
                trycount++;
                try
                {
                    if (driver.FindElement(By.Id(id)) != null)
                    {
                        chkEle = false;
                    }
                }
                catch (Exception)
                {
                }
                if (trycount > 10)
                {
                    chkEle = false;
                    return false;
                }
            } while (chkEle);
            return true;
        }
        public Boolean rtnifElementxpath(string xpath)
        {
            Boolean chkEle = true;
            int trycount = 0;
            do
            {
                Thread.Sleep(500);
                trycount++;
                try
                {
                    if (driver.FindElement(By.XPath(xpath)) != null)
                    {
                        chkEle = false;
                    }
                }
                catch (Exception)
                {
                }
                if (trycount > 30)
                {
                    chkEle = false;
                    return false;
                }
            } while (chkEle);
            return true;
        }
        public string RandomString(int int_NumberLength)
        {
            System.Text.StringBuilder ps_Code = new System.Text.StringBuilder();
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            // 亂數物件，隨機產生英數字元 fortify(Insecure Randomness): 自行創建randomseed值
            Random rngp = new Random();
            for (int i = 1; i <= int_NumberLength; i++)
            {
                switch (rngp.Next(0, 1))
                {
                    case 0:
                        {
                            // 產生英文大寫的亂數
                            ps_Code.Append(Convert.ToChar(rngp.Next(0, 25) + 97));
                            break;
                        }

                    case 1:
                        {
                            // 產生英文小寫的亂數
                            ps_Code.Append(Convert.ToChar(rngp.Next(0, 25) + 65));
                            break;
                        }

                }
            }

            return ps_Code.ToString();
        }

    }
}