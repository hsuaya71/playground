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
        string logfilename = "";
        [ClassInitialize()]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(baseURL+"/uniagent/bmem_login.aspx");
        }
        [TestMethod]
        public void login() {
            driver.Navigate().GoToUrl(baseURL + "/uniagent/bmem_login.aspx");            
            logfilename = "[login]_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            sub_WriteTxt("UNIB2B登入", "bmem_login.aspx", logfilename);

            Thread.Sleep(1000);
            textinput("id","txtAgentUniNo","11223344");
            textinput("id", "txtUserId", "ayahsu");
            textinput("id", "txtPassword", "89562356a"); 
        }

        public Boolean chklogin() {
            if (driver.FindElement(By.Id("ui_t01")).Text == "最新公告")
            {
                return true;
            }
            else
                return false;
        }
        public void order1() {
            driver.Navigate().GoToUrl(baseURL + "/uniagent/dragon/FuncMenu_Dragon_F.aspx"); 
            driver.FindElement(By.LinkText("訂位新增")).Click();
            ddlselect("id", "DropDownList1", "松山-金門-廈門五通");
            textinput("id", "YearSt", "2020");
            textinput("id","MonthSt","08");
            textinput("id", "DaySt", "29");
            ddlselect("name", "ddm_adultNum", "1"); 
            //IWebElement submit = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#Button2')[0].click();");
            driver.FindElement(By.Id("Button2")).Click();
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
        private void ddlselect(string opt, string key, string value)
        {
            if (opt == "id")
            {
                IWebElement dropDownListBox = driver.FindElement(By.Id(key));
                SelectElement clickThis = new SelectElement(dropDownListBox);
                clickThis.SelectByText(value);
            }
            else if (opt == "name") { 
                IWebElement dropDownListBox = driver.FindElement(By.Name(key));
                SelectElement clickThis = new SelectElement(dropDownListBox);
                clickThis.SelectByText(value);
            }
        }
        private void textinput(string opt, string key, string value)
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
        private void sub_WriteTxt(string pS_String, string pS_method, string filename)
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
}