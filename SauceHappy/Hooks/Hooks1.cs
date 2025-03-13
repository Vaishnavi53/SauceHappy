using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace SauceHappy.Hooks1
{
    [Binding]
    public class Hooks
    {
        private static IWebDriver driver;
        private readonly ScenarioContext _scenarioContext;
        private static ExtentReports _extent;
        private static ExtentTest _feature;
        private ExtentTest _scenario;
        private static ExtentSparkReporter _sparkReporter;
        private static string reportPath;
        private static string screenshotsDir;
        private static List<string> failedScreenshots = new List<string>();

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
            
            string reportsDir = Path.Combine(Directory.GetCurrentDirectory(), "Reports");
            Directory.CreateDirectory(reportsDir);
            reportPath = Path.Combine(reportsDir, "ExtentReport.html");
            
            screenshotsDir = Path.Combine(reportsDir, "Screenshots");
            Directory.CreateDirectory(screenshotsDir);

            _sparkReporter = new ExtentSparkReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(_sparkReporter);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extent.CreateTest(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void Setup()
        {
            _scenario = _feature.CreateNode(_scenarioContext.ScenarioInfo.Title);
            _scenarioContext["WebDriver"] = driver;
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            string stepText = _scenarioContext.StepContext.StepInfo.Text;
            string screenshotPath = CaptureScreenshot(_scenarioContext.ScenarioInfo.Title, stepText);
            
            if (_scenarioContext.TestError == null)
            {
                _scenario.Log(Status.Pass, stepText);
            }
            else
            {
                if (screenshotPath != null)
                {
                    _scenario.Log(Status.Fail, stepText,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                    failedScreenshots.Add(screenshotPath);
                }
                else
                {
                    _scenario.Log(Status.Fail, stepText);
                }
                _scenario.Log(Status.Fail, _scenarioContext.TestError.Message);
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
            _extent.Flush();
            SendEmailWithGmail();
        }

        private string CaptureScreenshot(string scenarioName, string stepName)
        {
            try
            {
                if (driver == null || driver.WindowHandles.Count == 0)
                {
                    TestContext.Progress.WriteLine("No active browser window. Skipping screenshot.");
                    return null;
                }
                Thread.Sleep(500);
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                
                string sanitizedStepName = string.Join("_", stepName.Split(Path.GetInvalidFileNameChars()));
                string fileName = $"{scenarioName}_{sanitizedStepName}.png";
                string filePath = Path.Combine(screenshotsDir, fileName);
                
                screenshot.SaveAsFile(filePath);
                return filePath;
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine($"Failed to capture screenshot: {ex.Message}");
                return null;
            }
        }

        private static void SendEmailWithGmail()
        {
            try
            {
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string senderEmail = "vaishnavimbhat03@gmail.com";
                string senderPassword = "gadc mhyr pkpx jljf";
                string recipientEmail = "190230@sdmcujire.in";
                
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "SpecFlow Test Report",
                    Body = "Attached is the Extent Report and failed test screenshots from the latest execution.",
                    IsBodyHtml = false
                };
                
                mail.To.Add(recipientEmail);
                if (File.Exists(reportPath))
                {
                    mail.Attachments.Add(new Attachment(reportPath));
                    TestContext.Progress.WriteLine("✅ Attached Extent Report");
                }
                else
                {
                    TestContext.Progress.WriteLine("❌ Report file not found!");
                }
                
                foreach (var screenshot in failedScreenshots)
                {
                    if (File.Exists(screenshot))
                    {
                        mail.Attachments.Add(new Attachment(screenshot));
                        TestContext.Progress.WriteLine($"✅ Attached Screenshot: {screenshot}");
                    }
                }
                
                SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };
                
                smtp.Send(mail);
                TestContext.Progress.WriteLine("✅ Email sent successfully via Gmail SMTP!");
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine($"❌ Failed to send email via Gmail SMTP: {ex.Message}");
            }
        }
    }
}
