using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class EVALogin
    {
        private IWebDriver driver, IEdriver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        string weburl = "http://qaecweb03.evaair.com/enterpriseservice/";
        private static string msg = "";
        private string logfilename = "";
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            //driver = new OpenQA.Selenium.IE.InternetExplorerDriver();//
            baseURL = "http://qaecweb03.evaair.com";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        [Test]
        public string TheEva_ebms_05()
        {
            driver.Navigate().GoToUrl(baseURL + "/EBMS/Login.aspx");
            logfilename = "[login]_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            sub_WriteTxt("EBMS登入", "login.aspx", logfilename);

            driver.Navigate().GoToUrl(baseURL + "/EBMS/Login.aspx");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("TxtUserId")).Clear();
            driver.FindElement(By.Id("TxtUserId")).SendKeys("F78539t;6bj/6dj94xk4"); 
            IWebElement element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#BtnLogin')[0].click();");
            Thread.Sleep(3000);
            // F78539(徐士雅)
            var a = driver.FindElement(By.Id("LblUserInfo"));
            if (a.Text == "F78539(徐士雅)")
            {
                sub_WriteTxt("登入成功", "login.aspx", logfilename);
                element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePower tr td a')[0].click();");

                ebms_05();
            }
            driver.Close();
            return msg;
        }

        #region 開票紀錄及飛航記錄查詢
        private void ebms_05()
        {
            Thread.Sleep(1000);
            var b = driver.FindElement(By.Id("TreePowert5"));
            msg = "";
            if (b.Text == "開票紀錄及飛航記錄查詢")
            {
                string temp = "";
                sub_WriteTxt("開票紀錄及飛航記錄查詢", "login.aspx", logfilename);
                IWebElement element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert5')[0].click();");
                Thread.Sleep(500);
                compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM045.ASPX", "進入功能OK", "進入功能failed，連結:" + driver.Url, logfilename);
                Thread.Sleep(1000);
                //查詢票券號碼
                string tkt_nbr = "6951384465185";
                string corp_id = "HSZ0000096";
                string abbr_id = "TPE0497";
                try
                {
                    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_TKT_NBR")).SendKeys(tkt_nbr);
                    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                    Thread.Sleep(1000);
                    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl03_0")).Text;
                    compareIfEqual(tkt_nbr, temp, "查詢票券號碼'" + tkt_nbr + "'OK", "查詢票券號碼'" + tkt_nbr + "'failed，錯誤結果為:" + temp, logfilename);
                }
                catch (Exception ex)
                {
                    sub_WriteTxt("查詢票券號碼''" + tkt_nbr + "'failed, " + ex.ToString(), "", logfilename);
                }

                //查詢公司代碼&區間
                driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_ISSUE_START_DT_datepicker")).SendKeys("2017/05/04");
                driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_ISSUE_END_DT_datepicker")).SendKeys("2017/05/04");
                driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPORATE_CODE_Txt_Company")).SendKeys(corp_id);
                element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                Thread.Sleep(1000);
                temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl01_0")).Text;
                compareIfEqual(corp_id, temp, "查詢公司代碼&區間OK", "查詢公司代碼&區間failed，錯誤結果為:" + temp, logfilename);
                //查詢簡碼
                driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPORATE_CODE_Txt_Company")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TXT_ABBREVIATION")).SendKeys(abbr_id);
                element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                Thread.Sleep(1000);
                temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl02_0")).Text;
                compareIfEqual(abbr_id, temp, "查詢簡碼'" + abbr_id + "'OK", "查詢簡碼'" + abbr_id + "'failed，錯誤結果為:" + temp, logfilename);
                //查詢班機號碼
                //TODO 目前此功能有error，待測試
            }
            else
            {
                sub_WriteTxt("功能:開票紀錄及飛航記錄查詢，文字錯誤" + b.Text + "，連結錯誤" + driver.Url, "login.aspx", logfilename);
            }
        }
        #endregion
        [Test]
        public void TheEvaBackendTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/EBMS/Login.aspx");
            string logfilename = "[login]_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            sub_WriteTxt("EBMS登入", "login.aspx", logfilename);

            driver.Navigate().GoToUrl(baseURL + "/EBMS/Login.aspx");
            driver.FindElement(By.Id("TxtUserId")).Clear();
            driver.FindElement(By.Id("TxtUserId")).SendKeys("F78539t;6bj/6dj94xk4");
            IWebElement element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#BtnLogin')[0].click();");
            Thread.Sleep(3000);
            // F78539(徐士雅)
            var a = driver.FindElement(By.Id("LblUserInfo"));
            if (a.Text == "F78539(徐士雅)")
            {
                sub_WriteTxt("登入成功", "login.aspx", logfilename);
                element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePower tr td a')[0].click();");
                Thread.Sleep(1000);
                var b = driver.FindElement(By.Id("TreePowert1"));

                #region 企業會員資料維護
                //if (b.Text == "企業會員資料維護")
                //{
                //    string temp = "";
                //    sub_WriteTxt("企業會員資料維護", "login.aspx", logfilename);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert1')[0].click();");
                //    Thread.Sleep(500);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM010.aspx", "進入功能OK", "進入功能failed，連結:" + driver.Url, logfilename);

                //    //查詢公司名稱  
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).SendKeys("群光");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("群光電子股份有限公司 CHICONY", temp, "查詢公司名稱'群光'OK", "查詢公司名稱'群光'failed，錯誤結果為:" + temp, logfilename);

                //    //查詢公司代碼
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE")).SendKeys("83");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl01_0")).Text;
                //    compareIfEqual("CYI0000083", temp, "查詢公司代碼'83'OK", "查詢公司代碼'83'failed，錯誤結果為:" + temp, logfilename);

                //    //查詢公司簡碼  
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_BIZ_CD")).SendKeys("TPE0052");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("群光電子股份有限公司 CHICONY", temp, "查詢公司簡碼'TPE0052'OK", "查詢公司簡碼'TPE0052'failed，錯誤結果為:" + temp, logfilename);

                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_rpt1_ctl00_0')[0].click();");
                //    Thread.Sleep(1000);
                //    compareIfEqual("http://qaecweb03.evaair.com/EBMS/BZD/BZDM011.aspx?id=TPE0000083", driver.Url, "進入明細頁OK", "進入明細頁failed，錯誤:" + driver.Url, logfilename);

                //    //點擊save
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_SAVE')[0].click();");
                //    Thread.Sleep(500);
                //    doalertclick();
                //    Thread.Sleep(500);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_BACK')[0].click();");
                //    Thread.Sleep(1000);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM010.aspx", "返回功能OK", "返回功能failed，連結:" + driver.Url, logfilename);

                //}
                //else
                //{
                //    sub_WriteTxt("功能:企業會員資料維護，文字錯誤" + b.Text + "，連結錯誤" + driver.Url, "login.aspx", logfilename);
                //}
                #endregion

                #region 企業會員合約資料維護
                //Thread.Sleep(1000);
                //b = driver.FindElement(By.Id("TreePowert2"));
                //if (b.Text == "企業會員合約資料維護")
                //{
                //    string temp = "";
                //    sub_WriteTxt("企業會員合約資料維護", "login.aspx", logfilename);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert2')[0].click();");
                //    Thread.Sleep(500);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM020.aspx", "進入功能OK", "進入功能failed，連結:" + driver.Url, logfilename);

                //    //查詢公司名稱  
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).SendKeys("群光");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("群光電子股份有限公司 CHICONY", temp, "查詢公司名稱'群光'OK", "查詢公司名稱'群光'failed，錯誤結果為:" + temp, logfilename);

                //    //查詢公司代碼  
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE")).SendKeys("TPE0000083");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("群光電子股份有限公司 CHICONY", temp, "查詢公司代碼'TPE0000083'OK", "查詢公司代碼'TPE0000083'failed，錯誤結果為:" + temp, logfilename);


                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).SendKeys("群光");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("群光電子股份有限公司 CHICONY", temp, "查詢公司名稱及代碼'群光'OK", "查詢公司名稱及代碼'群光'failed，錯誤結果為:" + temp, logfilename);

                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_rpt1_ctl00_0')[0].click();");
                //    Thread.Sleep(1000);
                //    compareIfEqual("http://qaecweb03.evaair.com/EBMS/BZD/BZDM021.aspx?id=1149", driver.Url, "進入明細頁OK", "進入明細頁failed，錯誤:" + driver.Url, logfilename);

                //    //點擊save
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_SAVE')[0].click();");
                //    Thread.Sleep(500);
                //    doalertclick();
                //    Thread.Sleep(500);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_BACK')[0].click();");
                //    Thread.Sleep(1000);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM020.aspx", "返回功能OK", "返回功能failed，連結:" + driver.Url, logfilename);

                //}
                //else
                //{
                //    sub_WriteTxt("功能:企業會員合約資料維護，文字錯誤" + b.Text + "，連結錯誤" + driver.Url, "login.aspx", logfilename);
                //}
                #endregion

                #region 企業會員Revenue查詢
                //Thread.Sleep(1000);
                //b = driver.FindElement(By.Id("TreePowert3"));
                //if (b.Text == "企業會員Revenue查詢")
                //{
                //    string temp = "";
                //    sub_WriteTxt("企業會員Revenue查詢", "login.aspx", logfilename);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert3')[0].click();");
                //    Thread.Sleep(500);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM030.ASPX", "進入功能OK", "進入功能failed，連結:" + driver.Url, logfilename);

                //    //查詢公司名稱  
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).SendKeys("群光");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(5000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("群光電子股份有限公司 CHICONY", temp, "查詢公司名稱'群光'OK", "查詢公司名稱'群光'failed，錯誤結果為:" + temp, logfilename);

                //    //查詢公司代碼  
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE_Txt_Company")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE_Txt_Company")).SendKeys("TPE0000083");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("群光電子股份有限公司 CHICONY", temp, "查詢公司代碼'TPE0000083'OK", "查詢公司代碼'TPE0000083'failed，錯誤結果為:" + temp, logfilename);

                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_rpt1_ctl00_0')[0].click();");
                //    Thread.Sleep(2000);
                //    try
                //    {
                //        temp = driver.FindElement(By.Id("ContentPlaceHolder1_LBL_CORP_NAME")).Text;
                //        compareIfEqual("群光電子股份有限公司 CHICONY", temp, "進入明細頁OK", "進入明細頁failed，錯誤:" + driver.Url, logfilename);  
                //    }
                //    catch (Exception ex)
                //    {
                //        sub_WriteTxt("進入明細頁failed," + ex.ToString(), "", logfilename);
                //    }                

                //}
                //else
                //{
                //    sub_WriteTxt("功能:企業會員Revenue查詢，文字錯誤" + b.Text + "，連結錯誤" + driver.Url, "login.aspx", logfilename);
                //}
                #endregion

                #region 企業會員By Month Revenue查詢
                //Thread.Sleep(1000);
                //b = driver.FindElement(By.Id("TreePowert4"));
                //if (b.Text == "企業會員By Month Revenue查詢")
                //{
                //    string temp = "";
                //    sub_WriteTxt("企業會員By Month Revenue查詢", "login.aspx", logfilename);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert4')[0].click();");
                //    Thread.Sleep(500);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM035.ASPX", "進入功能OK", "進入功能failed，連結:" + driver.Url, logfilename);

                //    //查詢公司代碼&月區間 
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE_Txt_Company")).SendKeys("TPE0000083");
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_START_MN_datepicker")).SendKeys("2016/11");
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_END_MN_datepicker")).SendKeys("2017/02");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(2000);
                //    temp = driver.FindElement(By.XPath("//tr[@id='ContentPlaceHolder1_rpt1_Tr2_0']/td")).Text;
                //    compareIfEqual("2016/11/01", temp, "查詢結果OK", "查詢結果failed，錯誤結果為:" + temp, logfilename);
                //    Thread.Sleep(1000);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_CLEAR')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE_Txt_Company")).Text;
                //    compareIfEqual("", temp, "清空公司代碼OK", "清空公司代碼failed，錯誤結果為:" + temp, logfilename);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_TXT_START_MN_datepicker")).Text;
                //    compareIfEqual("", temp, "清空查詢起日OK", "清空查詢起日failed，錯誤結果為:" + temp, logfilename);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_TXT_END_MN_datepicker")).Text;
                //    compareIfEqual("", temp, "清空查詢迄日OK", "清空查詢迄日failed，錯誤結果為:" + temp, logfilename);

                //}
                //else
                //{
                //    sub_WriteTxt("功能:企業會員By Month Revenue查詢，文字錯誤" + b.Text + "，連結錯誤" + driver.Url, "login.aspx", logfilename);
                //}
                #endregion

                #region 開票紀錄及飛航記錄查詢
                Thread.Sleep(1000);
                b = driver.FindElement(By.Id("TreePowert5"));
                if (b.Text == "開票紀錄及飛航記錄查詢")
                {
                    string temp = "";
                    sub_WriteTxt("開票紀錄及飛航記錄查詢", "login.aspx", logfilename);
                    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert5')[0].click();");
                    Thread.Sleep(500);
                    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM045.ASPX", "進入功能OK", "進入功能failed，連結:" + driver.Url, logfilename);
                    Thread.Sleep(1000);
                    //查詢票券號碼
                    string tkt_nbr = "6951384465185";
                    string corp_id = "HSZ0000096";
                    string abbr_id = "TPE0497";
                    try
                    {
                        driver.FindElement(By.Id("ContentPlaceHolder1_TXT_TKT_NBR")).SendKeys(tkt_nbr);
                        element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                        Thread.Sleep(1000);
                        temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl03_0")).Text;
                        compareIfEqual(tkt_nbr, temp, "查詢票券號碼'" + tkt_nbr + "'OK", "查詢票券號碼'" + tkt_nbr + "'failed，錯誤結果為:" + temp, logfilename);
                    }
                    catch (Exception ex)
                    {
                        sub_WriteTxt("查詢票券號碼''"+tkt_nbr+"'failed, " + ex.ToString(), "", logfilename);
                    }

                    //查詢公司代碼&區間
                    driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_ISSUE_START_DT_datepicker")).SendKeys("2017/05/04");
                    driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_ISSUE_END_DT_datepicker")).SendKeys("2017/05/04");
                    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPORATE_CODE_Txt_Company")).SendKeys(corp_id);
                    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                    Thread.Sleep(1000);
                    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl01_0")).Text;
                    compareIfEqual(corp_id, temp, "查詢公司代碼&區間OK", "查詢公司代碼&區間failed，錯誤結果為:" + temp, logfilename);
                    //查詢簡碼
                    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPORATE_CODE_Txt_Company")).Clear();
                    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_ABBREVIATION")).SendKeys(abbr_id);
                    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                    Thread.Sleep(1000);
                    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl02_0")).Text;
                    compareIfEqual(abbr_id, temp, "查詢簡碼'" + abbr_id + "'OK", "查詢簡碼'" + abbr_id + "'failed，錯誤結果為:" + temp, logfilename);
                    //查詢班機號碼
                    //TODO 目前此功能有error，待測試
                }
                else
                {
                    sub_WriteTxt("功能:開票紀錄及飛航記錄查詢，文字錯誤" + b.Text + "，連結錯誤" + driver.Url, "login.aspx", logfilename);
                }
                #endregion

                #region 點數兌換項目維護
                //Thread.Sleep(1000);
                //b = driver.FindElement(By.Id("TreePowert6"));
                //if (b.Text == "點數兌換項目維護")
                //{
                //    string temp = "";
                //    sub_WriteTxt("點數兌換項目維護", "login.aspx", logfilename);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert6')[0].click();");
                //    Thread.Sleep(500);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM074.ASPX", "進入功能OK", "進入功能failed，連結:" + driver.Url, logfilename);

                //    //查詢Version=8
                //    Thread.Sleep(1000);
                //    driver.FindElement(By.Id("ContentPlaceHolder1_Ddl_POINTS_EXCHANGE_VERSION_NBR")).SendKeys("8");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("8", temp, "查詢Version='8'OK", "查詢Version='8'failed，錯誤結果為:" + temp, logfilename);
                //    //查詢生效日期區間
                //    Thread.Sleep(1000);
                //    driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_EFFECT_DT_datepicker")).SendKeys("2016/02/01");
                //    driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_EXPIRE_DT_datepicker")).SendKeys("2017/08/01");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl01_0")).Text;
                //    compareIfEqual("2016/07/01", temp, "查詢生效日期區間OK", "查詢生效日期區間failed，錯誤結果為:" + temp, logfilename);
                //    //查詢已審核
                //    Thread.Sleep(1000);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_RDOBLT_APPROV_1')[0].click();");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl04_0")).Text;
                //    compareIfEqual("Y", temp, "查詢已審核OK", "查詢已審核failed，錯誤結果為:" + temp, logfilename);

                //    //clear按鈕
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_CLEAR')[0].click();");
                //    Thread.Sleep(1000);

                //    var ddl = driver.FindElement(By.Id("ContentPlaceHolder1_Ddl_POINTS_EXCHANGE_VERSION_NBR"));
                //    var se = new SelectElement(ddl);
                //    temp = se.SelectedOption.Text;
                //    compareIfEqual("--請選擇--", temp, "clear按鈕 OK", "clear按鈕 failed，錯誤結果為:" + temp, logfilename);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_EFFECT_DT_datepicker")).Text;
                //    compareIfEqual("", temp, "clear按鈕 OK", "clear按鈕 failed，錯誤結果為:" + temp, logfilename);

                                                             
                //}
                //else
                //{
                //    sub_WriteTxt("功能:點數兌換項目維護，文字錯誤" + b.Text + "，連結錯誤" + driver.Url, "login.aspx", logfilename);
                //}
                #endregion
            }
            else
                sub_WriteTxt("登入失敗", "login.aspx", logfilename);
            driver.Close();
        }

        private void compareIfEqual(string str1, string str2, string okMsg, string failMsg, string logfilename)
        {
            Thread.Sleep(1000);
            if (str1 == str2)
                sub_WriteTxt(okMsg, "login.aspx", logfilename);
            else
                sub_WriteTxt(failMsg, "login.aspx", logfilename);
            Thread.Sleep(1000);
        }
        
        [Test]
        public void TheEVALoginTest()
        { 

            //驗證首頁的連結
            string logfilename = "[login]_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            //sub_WriteTxt("login.aspx", "login.aspx", logfilename);
            //driver.Navigate().GoToUrl(baseURL + "/enterpriseservice/login.aspx");
            //footerLinkCheck(logfilename);

            sub_WriteTxt("首頁 >  線上申請", "login.aspx", logfilename);
            driver.Navigate().GoToUrl(baseURL + "/enterpriseservice/OnlineApplication/CFFPM010.aspx");
            footerLinkCheck(logfilename);
            //gobackafterclick("$('#A1')[0].click();", "OnlineApplication/CFFPM010.aspx", "上方menu > 線上申請", logfilename, "/enterpriseservice/OnlineApplication/CFFPM010.aspx");
            //gobackafterclick("$('#A2')[0].click();", "application_step1.aspx", "上方menu > 專案介紹", logfilename, "/enterpriseservice/OnlineApplication/CFFPM010.aspx");
            //gobackafterclick("$('.breadcrumbul li .green')[0].click();", "Login.aspx", "麵包屑 首頁", logfilename, "/enterpriseservice/OnlineApplication/CFFPM010.aspx");
            //gobackafterclick("$('.breadcrumbul li .f_gold')[0].click();", "OnlineApplication/CFFPM010.aspx#", "麵包屑 線上申請", logfilename, "/enterpriseservice/OnlineApplication/CFFPM010.aspx");
            //gobackafterclick("$('.no_scroll.f14.top_head a')[0].click();", "Login.aspx", "上方Eva Air圖片", logfilename, "/enterpriseservice/OnlineApplication/CFFPM010.aspx");

            //sub_WriteTxt("首頁 >  專案介紹", "login.aspx", logfilename);
            //driver.Navigate().GoToUrl(baseURL + "/enterpriseservice/application_step1.aspx");
            //footerLinkCheck(logfilename);
            //gobackafterclick("$('.go_right li a')[0].click();", "OnlineApplication/CFFPM010.aspx?id=New", "上方menu > 線上申請", logfilename, "/enterpriseservice/application_step1.aspx");
            //gobackafterclick("$('.go_right li a')[1].click();", "application_step1.aspx#", "上方menu > 專案介紹", logfilename, "/enterpriseservice/application_step1.aspx");
            //gobackafterclick("$('.breadcrumbul li .green')[0].click();", "Login.aspx", "麵包屑 首頁", logfilename, "/enterpriseservice/application_step1.aspx");
            //gobackafterclick("$('.breadcrumbul li .f_gold')[0].click();", "application_step1.aspx#", "麵包屑 專案介紹", logfilename, "/enterpriseservice/application_step1.aspx");
            //gobackafterclick("$('.no_scroll.f14.top_head a')[0].click();", "Login.aspx", "上方Eva Air圖片", logfilename, "/enterpriseservice/application_step1.aspx");

            //login(logfilename);
            //sub_WriteTxt("會員專區 >  即時回饋", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/MemberInfo/CFFPM020.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("會員專區 >  即時回饋  >  歷年回饋點數兌換紀錄", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/MemberInfo/CFFPM021.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("會員專區 >  基本資訊", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/MemberInfo/CFFPM040.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("會員專區 >  基本資訊  >  修改密碼", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/MemberInfo/CFFPM041.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("會員專區 >  基本資訊  >  修改公司資訊", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/MemberInfo/CFFPM042.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("會員專區 > ECPP加值禮遇", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/MemberInfo/CFFPM043.aspx", logfilename);

            //login(logfilename);
            //sub_WriteTxt("即時回饋 >  使用說明", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/ExchangePrizes/rebate_1.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("即時回饋 > 我要兌換", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/ExchangePrizes/CFFPM030.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("即時回饋 > 設定兌換者", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/InstantFeedback/CFFPM050.aspx", logfilename);

            //login(logfilename);
            //sub_WriteTxt("ECPP加值禮遇 > 使用說明", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/ECPPAssignPerson/ecpp_first.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("ECPP加值禮遇 > 加值禮遇名單管理", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/ECPPAssignPerson/CFFPM080.aspx", logfilename);

            //login(logfilename);
            //sub_WriteTxt("最新消息", "login.aspx", logfilename);
            //tracePageLink("/EnterpriseService/news.aspx", logfilename); 

            driver.Close();
        }
        private void login(string logfilename)
        {
            driver.Navigate().GoToUrl(baseURL + "/enterpriseservice/login.aspx");
            driver.FindElement(By.Id("TxtUserId")).Clear();
            driver.FindElement(By.Id("TxtUserId")).SendKeys("TPE0900517");
            driver.FindElement(By.Id("TxtPassword")).Clear();
            driver.FindElement(By.Id("TxtPassword")).SendKeys("TPE0900517");
            IWebElement element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#BtnLogin')[0].click();");
            Thread.Sleep(500);
            sub_WriteTxt("登入成功：", "login.aspx", logfilename);
        }
        private void tracePageLink(string pagelink,string logfilename)
        {
            try
            {                
                driver.Navigate().GoToUrl(baseURL + pagelink);
                Thread.Sleep(500); 
                //會有跳窗
                if (pagelink == "/enterpriseservice/ECPPAssignPerson/CFFPM080.aspx")
                    doalertclick();
                footerLinkCheck(logfilename);
                //menu連結 
                gobackafterclick("$('.f14 ul li a')[2].click();", "ExchangePrizes/rebate_1.aspx", "上方menu > 即時回饋", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[3].click();", "ExchangePrizes/rebate_1.aspx", "上方menu > 即時回饋 > 使用說明", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[4].click();", "ExchangePrizes/CFFPM030.aspx", "上方menu > 即時回饋 > 我要兌換", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[5].click();", "InstantFeedback/CFFPM050.aspx", "上方menu > 即時回饋 > 設定兌換者", logfilename, pagelink);
                gobackafterclick("$('.f14 ul li a')[3].click();", "ECPPAssignPerson/ecpp_first.aspx", "上方menu > ECPP加值禮遇", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[6].click();", "ECPPAssignPerson/ecpp_first.aspx", "上方menu > ECPP加值禮遇 >  使用說明", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[7].click();", "ECPPAssignPerson/CFFPM080.aspx", "上方menu > ECPP加值禮遇  >  加值禮遇名單管理", logfilename, pagelink);
                gobackafterclick("$('.f14 ul li a')[4].click();", "news.aspx", "上方menu > 最新消息", logfilename, pagelink);
                gobackafterclick("$('.f14 ul li a')[0].click();", "MemberInfo/CFFPM040.aspx", "上方menu > 會員專區", logfilename,pagelink);
                gobackafterclick("$('.f14 .f13 a')[0].click();", "MemberInfo/CFFPM040.aspx", "上方menu > 會員專區 > 基本資訊", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[1].click();", "MemberInfo/CFFPM020.aspx", "上方menu > 會員專區 > 即時回饋", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[2].click();", "MemberInfo/CFFPM043.aspx", "上方menu > 會員專區 >  ECPP加值禮遇", logfilename, pagelink);
                gobackafterclick("$('.no_scroll.f14.top_head a')[0].click();", "MemberInfo/CFFPM020.aspx", "上方Eva Air圖片", logfilename, pagelink);
                gobackafterclick("$('.f14 ul li a')[1].click();", "Login.aspx", "上方menu > 登出", logfilename, pagelink);               
                     
            }
            catch (Exception ex)
            {
                sub_WriteTxt("執行失敗，錯誤訊息：" + ex.ToString(), "login.aspx", logfilename);
                return;
            }
        }
        private void footerLinkCheck(string logfilename)
        { 
                //footer連結
                clickandclosetab("$('.f_gold .f_white')[0].click();", "https://www.evaair.com/zh-tw/index.html", "另開EVA air網頁", logfilename);
                clickandclosetab("$('.f_gold .f_white')[1].click();", "https://booking.evaair.com/flyeva/EVA/B2C/flight-schedules.aspx", "另開航班資訊網頁", logfilename);
                clickandclosetab("$('.f_gold .f_white')[2].click();", "https://eservice.evaair.com/flyeva/EVA/FFP/login.aspx", "另開無限萬哩遊網頁", logfilename);
                clickandclosetab("$('.f_gold .f_white')[3].click();", "http://www.evaair.com/zh-tw/booking-and-travel-planning/promotion/", "另開超值優惠網頁", logfilename);
                clickandclosetab("$('.f_gold .f_white')[4].click();", "http://www.evaair.com/zh-tw/contact-us-and-help/contact-us/", "另開聯絡資訊網頁", logfilename);  
        }
        private void doalertclick() {
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }
        private void clickandclosetab(string p1, string p2, string p3, string logfilename)
        {
            try
            {
                IWebElement element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript(p1);
                Thread.Sleep(1000);
                driver.SwitchTo().Window(driver.WindowHandles[1]);
                if (driver.Url == p2)
                    sub_WriteTxt(p3+" OK", "login.aspx", logfilename);
                else
                    sub_WriteTxt(p3+" failed: "+driver.Url, "login.aspx", logfilename);
                driver.SwitchTo().Window(driver.WindowHandles[1]).Close();
                Thread.Sleep(500);
                driver.SwitchTo().Window(driver.WindowHandles[0]);

            }
            catch (Exception ex)
            {
                sub_WriteTxt("clickandclosetab /"+p3+"執行失敗，錯誤訊息：" + ex.ToString(), "login.aspx", logfilename); 
            }
        }

        private void gobackafterclick(string p1, string p2, string p3, string logfilename, string gobackpage)
        {
            try
            {
                IWebElement element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript(p1);
                Thread.Sleep(1500);
                //會有跳窗
                if (p2 == "ECPPAssignPerson/CFFPM080.aspx")
                    doalertclick(); 
                if (driver.Url == (weburl + p2))
                    sub_WriteTxt(p3+" OK", "login.aspx", logfilename);
                else
                    sub_WriteTxt(p3+" failed: " + driver.Url, "login.aspx", logfilename);
                driver.Navigate().GoToUrl(baseURL + gobackpage); 
                Thread.Sleep(500);
                if (gobackpage == "/enterpriseservice/ECPPAssignPerson/CFFPM080.aspx")
                    doalertclick();
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                sub_WriteTxt("clickandclosetab /"+p3+"執行失敗，錯誤訊息：" + ex.ToString(), "login.aspx", logfilename); 
                 
            }
           
        }


        #region Log處理
        /// <summary>
        /// 寫入Log檔
        /// </summary>
        ///  
        static void sub_WriteTxt(string pS_String, string pS_method)
        {
            string pS_FileName;
            string pS_Log_Time = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string pS_LogPath = "C:/Users/F78539/Documents/Visual Studio 2012/Projects/F78539/Log/Log";
            string pS_YYYYMMDD = DateTime.Now.Date.ToString("yyyyMMdd");
            pS_LogPath = pS_LogPath + pS_YYYYMMDD + "\\";

            if (System.IO.Directory.Exists(pS_LogPath) == false)
            {
                System.IO.Directory.CreateDirectory(pS_LogPath);
            }

            pS_FileName = pS_LogPath + "EO_FIRSTUSE_MAIL_[" + pS_method + "]_" + pS_Log_Time + ".txt";
            System.IO.FileStream ss = new System.IO.FileStream(pS_FileName, System.IO.FileMode.Create);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(ss);
            msg += pS_String;
            sw.WriteLine(pS_String);
            sw.Close();
        }
        static void sub_WriteTxt(string pS_String, string pS_method, string filename)
        {
            string pS_FileName;
            string pS_Log_Time = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string pS_LogPath = "C:/Users/F78539/Documents/Visual Studio 2012/Projects/F78539/Log/";
            string pS_YYYYMMDD = DateTime.Now.Date.ToString("yyyyMMdd");
            pS_LogPath = pS_LogPath + pS_YYYYMMDD + "\\";
            pS_FileName = pS_LogPath+filename;

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
            else { 
                System.IO.StreamWriter sw = new System.IO.StreamWriter(pS_FileName, true);
                sw.WriteLine(pS_String);
                sw.Close();
            }
            msg += pS_String +"<br/>";
        }
        #endregion
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
