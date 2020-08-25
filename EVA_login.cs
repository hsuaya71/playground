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

            sub_WriteTxt("EBMS�n�J", "login.aspx", logfilename);

            driver.Navigate().GoToUrl(baseURL + "/EBMS/Login.aspx");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("TxtUserId")).Clear();
            driver.FindElement(By.Id("TxtUserId")).SendKeys("F78539t;6bj/6dj94xk4"); 
            IWebElement element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#BtnLogin')[0].click();");
            Thread.Sleep(3000);
            // F78539(�}�h��)
            var a = driver.FindElement(By.Id("LblUserInfo"));
            if (a.Text == "F78539(�}�h��)")
            {
                sub_WriteTxt("�n�J���\", "login.aspx", logfilename);
                element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePower tr td a')[0].click();");

                ebms_05();
            }
            driver.Close();
            return msg;
        }

        #region �}�������έ���O���d��
        private void ebms_05()
        {
            Thread.Sleep(1000);
            var b = driver.FindElement(By.Id("TreePowert5"));
            msg = "";
            if (b.Text == "�}�������έ���O���d��")
            {
                string temp = "";
                sub_WriteTxt("�}�������έ���O���d��", "login.aspx", logfilename);
                IWebElement element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert5')[0].click();");
                Thread.Sleep(500);
                compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM045.ASPX", "�i�J�\��OK", "�i�J�\��failed�A�s��:" + driver.Url, logfilename);
                Thread.Sleep(1000);
                //�d�߲��鸹�X
                string tkt_nbr = "6951384465185";
                string corp_id = "HSZ0000096";
                string abbr_id = "TPE0497";
                try
                {
                    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_TKT_NBR")).SendKeys(tkt_nbr);
                    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                    Thread.Sleep(1000);
                    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl03_0")).Text;
                    compareIfEqual(tkt_nbr, temp, "�d�߲��鸹�X'" + tkt_nbr + "'OK", "�d�߲��鸹�X'" + tkt_nbr + "'failed�A���~���G��:" + temp, logfilename);
                }
                catch (Exception ex)
                {
                    sub_WriteTxt("�d�߲��鸹�X''" + tkt_nbr + "'failed, " + ex.ToString(), "", logfilename);
                }

                //�d�ߤ��q�N�X&�϶�
                driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_ISSUE_START_DT_datepicker")).SendKeys("2017/05/04");
                driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_ISSUE_END_DT_datepicker")).SendKeys("2017/05/04");
                driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPORATE_CODE_Txt_Company")).SendKeys(corp_id);
                element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                Thread.Sleep(1000);
                temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl01_0")).Text;
                compareIfEqual(corp_id, temp, "�d�ߤ��q�N�X&�϶�OK", "�d�ߤ��q�N�X&�϶�failed�A���~���G��:" + temp, logfilename);
                //�d��²�X
                driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPORATE_CODE_Txt_Company")).Clear();
                driver.FindElement(By.Id("ContentPlaceHolder1_TXT_ABBREVIATION")).SendKeys(abbr_id);
                element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                Thread.Sleep(1000);
                temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl02_0")).Text;
                compareIfEqual(abbr_id, temp, "�d��²�X'" + abbr_id + "'OK", "�d��²�X'" + abbr_id + "'failed�A���~���G��:" + temp, logfilename);
                //�d�߯Z�����X
                //TODO �ثe���\�঳error�A�ݴ���
            }
            else
            {
                sub_WriteTxt("�\��:�}�������έ���O���d�ߡA��r���~" + b.Text + "�A�s�����~" + driver.Url, "login.aspx", logfilename);
            }
        }
        #endregion
        [Test]
        public void TheEvaBackendTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/EBMS/Login.aspx");
            string logfilename = "[login]_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            sub_WriteTxt("EBMS�n�J", "login.aspx", logfilename);

            driver.Navigate().GoToUrl(baseURL + "/EBMS/Login.aspx");
            driver.FindElement(By.Id("TxtUserId")).Clear();
            driver.FindElement(By.Id("TxtUserId")).SendKeys("F78539t;6bj/6dj94xk4");
            IWebElement element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#BtnLogin')[0].click();");
            Thread.Sleep(3000);
            // F78539(�}�h��)
            var a = driver.FindElement(By.Id("LblUserInfo"));
            if (a.Text == "F78539(�}�h��)")
            {
                sub_WriteTxt("�n�J���\", "login.aspx", logfilename);
                element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePower tr td a')[0].click();");
                Thread.Sleep(1000);
                var b = driver.FindElement(By.Id("TreePowert1"));

                #region ���~�|����ƺ��@
                //if (b.Text == "���~�|����ƺ��@")
                //{
                //    string temp = "";
                //    sub_WriteTxt("���~�|����ƺ��@", "login.aspx", logfilename);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert1')[0].click();");
                //    Thread.Sleep(500);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM010.aspx", "�i�J�\��OK", "�i�J�\��failed�A�s��:" + driver.Url, logfilename);

                //    //�d�ߤ��q�W��  
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).SendKeys("�s��");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("�s���q�l�ѥ��������q CHICONY", temp, "�d�ߤ��q�W��'�s��'OK", "�d�ߤ��q�W��'�s��'failed�A���~���G��:" + temp, logfilename);

                //    //�d�ߤ��q�N�X
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE")).SendKeys("83");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl01_0")).Text;
                //    compareIfEqual("CYI0000083", temp, "�d�ߤ��q�N�X'83'OK", "�d�ߤ��q�N�X'83'failed�A���~���G��:" + temp, logfilename);

                //    //�d�ߤ��q²�X  
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_BIZ_CD")).SendKeys("TPE0052");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("�s���q�l�ѥ��������q CHICONY", temp, "�d�ߤ��q²�X'TPE0052'OK", "�d�ߤ��q²�X'TPE0052'failed�A���~���G��:" + temp, logfilename);

                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_rpt1_ctl00_0')[0].click();");
                //    Thread.Sleep(1000);
                //    compareIfEqual("http://qaecweb03.evaair.com/EBMS/BZD/BZDM011.aspx?id=TPE0000083", driver.Url, "�i�J���ӭ�OK", "�i�J���ӭ�failed�A���~:" + driver.Url, logfilename);

                //    //�I��save
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_SAVE')[0].click();");
                //    Thread.Sleep(500);
                //    doalertclick();
                //    Thread.Sleep(500);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_BACK')[0].click();");
                //    Thread.Sleep(1000);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM010.aspx", "��^�\��OK", "��^�\��failed�A�s��:" + driver.Url, logfilename);

                //}
                //else
                //{
                //    sub_WriteTxt("�\��:���~�|����ƺ��@�A��r���~" + b.Text + "�A�s�����~" + driver.Url, "login.aspx", logfilename);
                //}
                #endregion

                #region ���~�|���X����ƺ��@
                //Thread.Sleep(1000);
                //b = driver.FindElement(By.Id("TreePowert2"));
                //if (b.Text == "���~�|���X����ƺ��@")
                //{
                //    string temp = "";
                //    sub_WriteTxt("���~�|���X����ƺ��@", "login.aspx", logfilename);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert2')[0].click();");
                //    Thread.Sleep(500);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM020.aspx", "�i�J�\��OK", "�i�J�\��failed�A�s��:" + driver.Url, logfilename);

                //    //�d�ߤ��q�W��  
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).SendKeys("�s��");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("�s���q�l�ѥ��������q CHICONY", temp, "�d�ߤ��q�W��'�s��'OK", "�d�ߤ��q�W��'�s��'failed�A���~���G��:" + temp, logfilename);

                //    //�d�ߤ��q�N�X  
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE")).SendKeys("TPE0000083");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("�s���q�l�ѥ��������q CHICONY", temp, "�d�ߤ��q�N�X'TPE0000083'OK", "�d�ߤ��q�N�X'TPE0000083'failed�A���~���G��:" + temp, logfilename);


                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).SendKeys("�s��");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("�s���q�l�ѥ��������q CHICONY", temp, "�d�ߤ��q�W�٤ΥN�X'�s��'OK", "�d�ߤ��q�W�٤ΥN�X'�s��'failed�A���~���G��:" + temp, logfilename);

                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_rpt1_ctl00_0')[0].click();");
                //    Thread.Sleep(1000);
                //    compareIfEqual("http://qaecweb03.evaair.com/EBMS/BZD/BZDM021.aspx?id=1149", driver.Url, "�i�J���ӭ�OK", "�i�J���ӭ�failed�A���~:" + driver.Url, logfilename);

                //    //�I��save
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_SAVE')[0].click();");
                //    Thread.Sleep(500);
                //    doalertclick();
                //    Thread.Sleep(500);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_BACK')[0].click();");
                //    Thread.Sleep(1000);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM020.aspx", "��^�\��OK", "��^�\��failed�A�s��:" + driver.Url, logfilename);

                //}
                //else
                //{
                //    sub_WriteTxt("�\��:���~�|���X����ƺ��@�A��r���~" + b.Text + "�A�s�����~" + driver.Url, "login.aspx", logfilename);
                //}
                #endregion

                #region ���~�|��Revenue�d��
                //Thread.Sleep(1000);
                //b = driver.FindElement(By.Id("TreePowert3"));
                //if (b.Text == "���~�|��Revenue�d��")
                //{
                //    string temp = "";
                //    sub_WriteTxt("���~�|��Revenue�d��", "login.aspx", logfilename);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert3')[0].click();");
                //    Thread.Sleep(500);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM030.ASPX", "�i�J�\��OK", "�i�J�\��failed�A�s��:" + driver.Url, logfilename);

                //    //�d�ߤ��q�W��  
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).SendKeys("�s��");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(5000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("�s���q�l�ѥ��������q CHICONY", temp, "�d�ߤ��q�W��'�s��'OK", "�d�ߤ��q�W��'�s��'failed�A���~���G��:" + temp, logfilename);

                //    //�d�ߤ��q�N�X  
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPNAME")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE_Txt_Company")).Clear();
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE_Txt_Company")).SendKeys("TPE0000083");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("�s���q�l�ѥ��������q CHICONY", temp, "�d�ߤ��q�N�X'TPE0000083'OK", "�d�ߤ��q�N�X'TPE0000083'failed�A���~���G��:" + temp, logfilename);

                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_rpt1_ctl00_0')[0].click();");
                //    Thread.Sleep(2000);
                //    try
                //    {
                //        temp = driver.FindElement(By.Id("ContentPlaceHolder1_LBL_CORP_NAME")).Text;
                //        compareIfEqual("�s���q�l�ѥ��������q CHICONY", temp, "�i�J���ӭ�OK", "�i�J���ӭ�failed�A���~:" + driver.Url, logfilename);  
                //    }
                //    catch (Exception ex)
                //    {
                //        sub_WriteTxt("�i�J���ӭ�failed," + ex.ToString(), "", logfilename);
                //    }                

                //}
                //else
                //{
                //    sub_WriteTxt("�\��:���~�|��Revenue�d�ߡA��r���~" + b.Text + "�A�s�����~" + driver.Url, "login.aspx", logfilename);
                //}
                #endregion

                #region ���~�|��By Month Revenue�d��
                //Thread.Sleep(1000);
                //b = driver.FindElement(By.Id("TreePowert4"));
                //if (b.Text == "���~�|��By Month Revenue�d��")
                //{
                //    string temp = "";
                //    sub_WriteTxt("���~�|��By Month Revenue�d��", "login.aspx", logfilename);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert4')[0].click();");
                //    Thread.Sleep(500);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM035.ASPX", "�i�J�\��OK", "�i�J�\��failed�A�s��:" + driver.Url, logfilename);

                //    //�d�ߤ��q�N�X&��϶� 
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE_Txt_Company")).SendKeys("TPE0000083");
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_START_MN_datepicker")).SendKeys("2016/11");
                //    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_END_MN_datepicker")).SendKeys("2017/02");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(2000);
                //    temp = driver.FindElement(By.XPath("//tr[@id='ContentPlaceHolder1_rpt1_Tr2_0']/td")).Text;
                //    compareIfEqual("2016/11/01", temp, "�d�ߵ��GOK", "�d�ߵ��Gfailed�A���~���G��:" + temp, logfilename);
                //    Thread.Sleep(1000);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_CLEAR')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPCODE_Txt_Company")).Text;
                //    compareIfEqual("", temp, "�M�Ť��q�N�XOK", "�M�Ť��q�N�Xfailed�A���~���G��:" + temp, logfilename);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_TXT_START_MN_datepicker")).Text;
                //    compareIfEqual("", temp, "�M�Ŭd�߰_��OK", "�M�Ŭd�߰_��failed�A���~���G��:" + temp, logfilename);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_TXT_END_MN_datepicker")).Text;
                //    compareIfEqual("", temp, "�M�Ŭd�ߨ���OK", "�M�Ŭd�ߨ���failed�A���~���G��:" + temp, logfilename);

                //}
                //else
                //{
                //    sub_WriteTxt("�\��:���~�|��By Month Revenue�d�ߡA��r���~" + b.Text + "�A�s�����~" + driver.Url, "login.aspx", logfilename);
                //}
                #endregion

                #region �}�������έ���O���d��
                Thread.Sleep(1000);
                b = driver.FindElement(By.Id("TreePowert5"));
                if (b.Text == "�}�������έ���O���d��")
                {
                    string temp = "";
                    sub_WriteTxt("�}�������έ���O���d��", "login.aspx", logfilename);
                    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert5')[0].click();");
                    Thread.Sleep(500);
                    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM045.ASPX", "�i�J�\��OK", "�i�J�\��failed�A�s��:" + driver.Url, logfilename);
                    Thread.Sleep(1000);
                    //�d�߲��鸹�X
                    string tkt_nbr = "6951384465185";
                    string corp_id = "HSZ0000096";
                    string abbr_id = "TPE0497";
                    try
                    {
                        driver.FindElement(By.Id("ContentPlaceHolder1_TXT_TKT_NBR")).SendKeys(tkt_nbr);
                        element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                        Thread.Sleep(1000);
                        temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl03_0")).Text;
                        compareIfEqual(tkt_nbr, temp, "�d�߲��鸹�X'" + tkt_nbr + "'OK", "�d�߲��鸹�X'" + tkt_nbr + "'failed�A���~���G��:" + temp, logfilename);
                    }
                    catch (Exception ex)
                    {
                        sub_WriteTxt("�d�߲��鸹�X''"+tkt_nbr+"'failed, " + ex.ToString(), "", logfilename);
                    }

                    //�d�ߤ��q�N�X&�϶�
                    driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_ISSUE_START_DT_datepicker")).SendKeys("2017/05/04");
                    driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_ISSUE_END_DT_datepicker")).SendKeys("2017/05/04");
                    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPORATE_CODE_Txt_Company")).SendKeys(corp_id);
                    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                    Thread.Sleep(1000);
                    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl01_0")).Text;
                    compareIfEqual(corp_id, temp, "�d�ߤ��q�N�X&�϶�OK", "�d�ߤ��q�N�X&�϶�failed�A���~���G��:" + temp, logfilename);
                    //�d��²�X
                    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_CORPORATE_CODE_Txt_Company")).Clear();
                    driver.FindElement(By.Id("ContentPlaceHolder1_TXT_ABBREVIATION")).SendKeys(abbr_id);
                    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                    Thread.Sleep(1000);
                    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl02_0")).Text;
                    compareIfEqual(abbr_id, temp, "�d��²�X'" + abbr_id + "'OK", "�d��²�X'" + abbr_id + "'failed�A���~���G��:" + temp, logfilename);
                    //�d�߯Z�����X
                    //TODO �ثe���\�঳error�A�ݴ���
                }
                else
                {
                    sub_WriteTxt("�\��:�}�������έ���O���d�ߡA��r���~" + b.Text + "�A�s�����~" + driver.Url, "login.aspx", logfilename);
                }
                #endregion

                #region �I�ƧI�����غ��@
                //Thread.Sleep(1000);
                //b = driver.FindElement(By.Id("TreePowert6"));
                //if (b.Text == "�I�ƧI�����غ��@")
                //{
                //    string temp = "";
                //    sub_WriteTxt("�I�ƧI�����غ��@", "login.aspx", logfilename);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#TreePowert6')[0].click();");
                //    Thread.Sleep(500);
                //    compareIfEqual(driver.Url, baseURL + "/EBMS/BZD/BZDM074.ASPX", "�i�J�\��OK", "�i�J�\��failed�A�s��:" + driver.Url, logfilename);

                //    //�d��Version=8
                //    Thread.Sleep(1000);
                //    driver.FindElement(By.Id("ContentPlaceHolder1_Ddl_POINTS_EXCHANGE_VERSION_NBR")).SendKeys("8");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl00_0")).Text;
                //    compareIfEqual("8", temp, "�d��Version='8'OK", "�d��Version='8'failed�A���~���G��:" + temp, logfilename);
                //    //�d�ߥͮĤ���϶�
                //    Thread.Sleep(1000);
                //    driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_EFFECT_DT_datepicker")).SendKeys("2016/02/01");
                //    driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_EXPIRE_DT_datepicker")).SendKeys("2017/08/01");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl01_0")).Text;
                //    compareIfEqual("2016/07/01", temp, "�d�ߥͮĤ���϶�OK", "�d�ߥͮĤ���϶�failed�A���~���G��:" + temp, logfilename);
                //    //�d�ߤw�f��
                //    Thread.Sleep(1000);
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_RDOBLT_APPROV_1')[0].click();");
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_QRY')[0].click();");
                //    Thread.Sleep(1000);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_rpt1_ctl04_0")).Text;
                //    compareIfEqual("Y", temp, "�d�ߤw�f��OK", "�d�ߤw�f��failed�A���~���G��:" + temp, logfilename);

                //    //clear���s
                //    element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("$('#ContentPlaceHolder1_BTN_CLEAR')[0].click();");
                //    Thread.Sleep(1000);

                //    var ddl = driver.FindElement(By.Id("ContentPlaceHolder1_Ddl_POINTS_EXCHANGE_VERSION_NBR"));
                //    var se = new SelectElement(ddl);
                //    temp = se.SelectedOption.Text;
                //    compareIfEqual("--�п��--", temp, "clear���s OK", "clear���s failed�A���~���G��:" + temp, logfilename);
                //    temp = driver.FindElement(By.Id("ContentPlaceHolder1_JWdate_EFFECT_DT_datepicker")).Text;
                //    compareIfEqual("", temp, "clear���s OK", "clear���s failed�A���~���G��:" + temp, logfilename);

                                                             
                //}
                //else
                //{
                //    sub_WriteTxt("�\��:�I�ƧI�����غ��@�A��r���~" + b.Text + "�A�s�����~" + driver.Url, "login.aspx", logfilename);
                //}
                #endregion
            }
            else
                sub_WriteTxt("�n�J����", "login.aspx", logfilename);
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

            //���ҭ������s��
            string logfilename = "[login]_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            //sub_WriteTxt("login.aspx", "login.aspx", logfilename);
            //driver.Navigate().GoToUrl(baseURL + "/enterpriseservice/login.aspx");
            //footerLinkCheck(logfilename);

            sub_WriteTxt("���� >  �u�W�ӽ�", "login.aspx", logfilename);
            driver.Navigate().GoToUrl(baseURL + "/enterpriseservice/OnlineApplication/CFFPM010.aspx");
            footerLinkCheck(logfilename);
            //gobackafterclick("$('#A1')[0].click();", "OnlineApplication/CFFPM010.aspx", "�W��menu > �u�W�ӽ�", logfilename, "/enterpriseservice/OnlineApplication/CFFPM010.aspx");
            //gobackafterclick("$('#A2')[0].click();", "application_step1.aspx", "�W��menu > �M�פ���", logfilename, "/enterpriseservice/OnlineApplication/CFFPM010.aspx");
            //gobackafterclick("$('.breadcrumbul li .green')[0].click();", "Login.aspx", "�ѥ]�h ����", logfilename, "/enterpriseservice/OnlineApplication/CFFPM010.aspx");
            //gobackafterclick("$('.breadcrumbul li .f_gold')[0].click();", "OnlineApplication/CFFPM010.aspx#", "�ѥ]�h �u�W�ӽ�", logfilename, "/enterpriseservice/OnlineApplication/CFFPM010.aspx");
            //gobackafterclick("$('.no_scroll.f14.top_head a')[0].click();", "Login.aspx", "�W��Eva Air�Ϥ�", logfilename, "/enterpriseservice/OnlineApplication/CFFPM010.aspx");

            //sub_WriteTxt("���� >  �M�פ���", "login.aspx", logfilename);
            //driver.Navigate().GoToUrl(baseURL + "/enterpriseservice/application_step1.aspx");
            //footerLinkCheck(logfilename);
            //gobackafterclick("$('.go_right li a')[0].click();", "OnlineApplication/CFFPM010.aspx?id=New", "�W��menu > �u�W�ӽ�", logfilename, "/enterpriseservice/application_step1.aspx");
            //gobackafterclick("$('.go_right li a')[1].click();", "application_step1.aspx#", "�W��menu > �M�פ���", logfilename, "/enterpriseservice/application_step1.aspx");
            //gobackafterclick("$('.breadcrumbul li .green')[0].click();", "Login.aspx", "�ѥ]�h ����", logfilename, "/enterpriseservice/application_step1.aspx");
            //gobackafterclick("$('.breadcrumbul li .f_gold')[0].click();", "application_step1.aspx#", "�ѥ]�h �M�פ���", logfilename, "/enterpriseservice/application_step1.aspx");
            //gobackafterclick("$('.no_scroll.f14.top_head a')[0].click();", "Login.aspx", "�W��Eva Air�Ϥ�", logfilename, "/enterpriseservice/application_step1.aspx");

            //login(logfilename);
            //sub_WriteTxt("�|���M�� >  �Y�ɦ^�X", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/MemberInfo/CFFPM020.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("�|���M�� >  �Y�ɦ^�X  >  ���~�^�X�I�ƧI������", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/MemberInfo/CFFPM021.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("�|���M�� >  �򥻸�T", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/MemberInfo/CFFPM040.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("�|���M�� >  �򥻸�T  >  �ק�K�X", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/MemberInfo/CFFPM041.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("�|���M�� >  �򥻸�T  >  �ק綠�q��T", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/MemberInfo/CFFPM042.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("�|���M�� > ECPP�[��§�J", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/MemberInfo/CFFPM043.aspx", logfilename);

            //login(logfilename);
            //sub_WriteTxt("�Y�ɦ^�X >  �ϥλ���", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/ExchangePrizes/rebate_1.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("�Y�ɦ^�X > �ڭn�I��", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/ExchangePrizes/CFFPM030.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("�Y�ɦ^�X > �]�w�I����", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/InstantFeedback/CFFPM050.aspx", logfilename);

            //login(logfilename);
            //sub_WriteTxt("ECPP�[��§�J > �ϥλ���", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/ECPPAssignPerson/ecpp_first.aspx", logfilename);
            //login(logfilename);
            //sub_WriteTxt("ECPP�[��§�J > �[��§�J�W��޲z", "login.aspx", logfilename);
            //tracePageLink("/enterpriseservice/ECPPAssignPerson/CFFPM080.aspx", logfilename);

            //login(logfilename);
            //sub_WriteTxt("�̷s����", "login.aspx", logfilename);
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
            sub_WriteTxt("�n�J���\�G", "login.aspx", logfilename);
        }
        private void tracePageLink(string pagelink,string logfilename)
        {
            try
            {                
                driver.Navigate().GoToUrl(baseURL + pagelink);
                Thread.Sleep(500); 
                //�|������
                if (pagelink == "/enterpriseservice/ECPPAssignPerson/CFFPM080.aspx")
                    doalertclick();
                footerLinkCheck(logfilename);
                //menu�s�� 
                gobackafterclick("$('.f14 ul li a')[2].click();", "ExchangePrizes/rebate_1.aspx", "�W��menu > �Y�ɦ^�X", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[3].click();", "ExchangePrizes/rebate_1.aspx", "�W��menu > �Y�ɦ^�X > �ϥλ���", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[4].click();", "ExchangePrizes/CFFPM030.aspx", "�W��menu > �Y�ɦ^�X > �ڭn�I��", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[5].click();", "InstantFeedback/CFFPM050.aspx", "�W��menu > �Y�ɦ^�X > �]�w�I����", logfilename, pagelink);
                gobackafterclick("$('.f14 ul li a')[3].click();", "ECPPAssignPerson/ecpp_first.aspx", "�W��menu > ECPP�[��§�J", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[6].click();", "ECPPAssignPerson/ecpp_first.aspx", "�W��menu > ECPP�[��§�J >  �ϥλ���", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[7].click();", "ECPPAssignPerson/CFFPM080.aspx", "�W��menu > ECPP�[��§�J  >  �[��§�J�W��޲z", logfilename, pagelink);
                gobackafterclick("$('.f14 ul li a')[4].click();", "news.aspx", "�W��menu > �̷s����", logfilename, pagelink);
                gobackafterclick("$('.f14 ul li a')[0].click();", "MemberInfo/CFFPM040.aspx", "�W��menu > �|���M��", logfilename,pagelink);
                gobackafterclick("$('.f14 .f13 a')[0].click();", "MemberInfo/CFFPM040.aspx", "�W��menu > �|���M�� > �򥻸�T", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[1].click();", "MemberInfo/CFFPM020.aspx", "�W��menu > �|���M�� > �Y�ɦ^�X", logfilename, pagelink);
                gobackafterclick("$('.f14 .f13 a')[2].click();", "MemberInfo/CFFPM043.aspx", "�W��menu > �|���M�� >  ECPP�[��§�J", logfilename, pagelink);
                gobackafterclick("$('.no_scroll.f14.top_head a')[0].click();", "MemberInfo/CFFPM020.aspx", "�W��Eva Air�Ϥ�", logfilename, pagelink);
                gobackafterclick("$('.f14 ul li a')[1].click();", "Login.aspx", "�W��menu > �n�X", logfilename, pagelink);               
                     
            }
            catch (Exception ex)
            {
                sub_WriteTxt("���楢�ѡA���~�T���G" + ex.ToString(), "login.aspx", logfilename);
                return;
            }
        }
        private void footerLinkCheck(string logfilename)
        { 
                //footer�s��
                clickandclosetab("$('.f_gold .f_white')[0].click();", "https://www.evaair.com/zh-tw/index.html", "�t�}EVA air����", logfilename);
                clickandclosetab("$('.f_gold .f_white')[1].click();", "https://booking.evaair.com/flyeva/EVA/B2C/flight-schedules.aspx", "�t�}��Z��T����", logfilename);
                clickandclosetab("$('.f_gold .f_white')[2].click();", "https://eservice.evaair.com/flyeva/EVA/FFP/login.aspx", "�t�}�L���U���C����", logfilename);
                clickandclosetab("$('.f_gold .f_white')[3].click();", "http://www.evaair.com/zh-tw/booking-and-travel-planning/promotion/", "�t�}�W���u�f����", logfilename);
                clickandclosetab("$('.f_gold .f_white')[4].click();", "http://www.evaair.com/zh-tw/contact-us-and-help/contact-us/", "�t�}�p����T����", logfilename);  
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
                sub_WriteTxt("clickandclosetab /"+p3+"���楢�ѡA���~�T���G" + ex.ToString(), "login.aspx", logfilename); 
            }
        }

        private void gobackafterclick(string p1, string p2, string p3, string logfilename, string gobackpage)
        {
            try
            {
                IWebElement element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript(p1);
                Thread.Sleep(1500);
                //�|������
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
                sub_WriteTxt("clickandclosetab /"+p3+"���楢�ѡA���~�T���G" + ex.ToString(), "login.aspx", logfilename); 
                 
            }
           
        }


        #region Log�B�z
        /// <summary>
        /// �g�JLog��
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
