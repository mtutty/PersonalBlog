using PersonalBlog.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Specialized;

namespace PersonalBlog.Tests
{
    
    /// <summary>
    ///This is a test class for ConfigSettingsServiceTest and is intended
    ///to contain all ConfigSettingsServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConfigSettingsServiceTest {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for ConfigSettingsService Constructor
        ///</summary>
        [TestMethod(), ExpectedException(typeof(NullReferenceException))]
        public void ConfigSettingsServiceNullConstructorTest() {
            NameValueCollection settings = null; // TODO: Initialize to an appropriate value
            ConfigSettingsService target = new ConfigSettingsService(settings);
        }

        /// <summary>
        ///A test for ConfigSettingsService Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void ConfigSettingsServiceConstructorTest() {
            NameValueCollection settings = new NameValueCollection();
            ConfigSettingsService target = new ConfigSettingsService(settings);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GetSetting
        ///</summary>
        [TestMethod()]
        public void GetSettingIntTest() {
            string key_a = @"test.a";
            string key_b = @"test.b";
            int value_a = 53;

            NameValueCollection settings = new NameValueCollection();
            settings.Add(key_a, value_a.ToString());

            ConfigSettingsService target = new ConfigSettingsService(settings);
            int expected = value_a;
            int actual = target.GetSetting(key_a, 0);
            Assert.AreEqual(expected, actual);

            expected = -1;
            actual = target.GetSetting(key_b, expected);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSetting
        ///</summary>
        [TestMethod()]
        public void GetSettingStringTest() {
            string key_a = @"test.a";
            string key_b = @"test.b";
            string value_a = @"abc123";

            NameValueCollection settings = new NameValueCollection();
            settings.Add(key_a, value_a);

            ConfigSettingsService target = new ConfigSettingsService(settings);
            string expected = value_a;
            string actual = target.GetSetting(key_a, string.Empty);
            Assert.AreEqual(expected, actual);

            expected = @"zZz";
            actual = target.GetSetting(key_b, expected);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSetting
        ///</summary>
        [TestMethod()]
        public void GetSettingStringNoDefaultTest() {
            string key_a = @"test.a";
            string key_b = @"test.b";
            string value_a = @"def345";

            NameValueCollection settings = new NameValueCollection();
            settings.Add(key_a, value_a);

            ConfigSettingsService target = new ConfigSettingsService(settings);
            string expected = value_a;
            string actual = target.GetSetting(key_a);
            Assert.AreEqual(expected, actual);

            actual = target.GetSetting(key_b);
            Assert.IsNull(actual);
        }
    }
}
