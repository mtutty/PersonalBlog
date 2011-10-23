using PersonalBlog.Services;
using NUnit.Framework;
using System;
using System.Collections.Specialized;

namespace PersonalBlog.Tests
{
    
    /// <summary>
    ///This is a test class for ConfigSettingsServiceTest and is intended
    ///to contain all ConfigSettingsServiceTest Unit Tests
    ///</summary>
    [TestFixture()]
    public class ConfigSettingsServiceTest : InjectedUnitTest {

        /// <summary>
        ///A test for ConfigSettingsService Constructor
        ///</summary>
        [Test()]
        public void ConfigSettingsServiceNullConstructorTest() {
            NameValueCollection settings = null;
            try {
                ConfigSettingsService target = new ConfigSettingsService(settings);
                Assert.Fail("Target method did not throw the expected exception.");
            } catch (NullReferenceException nrx) {
                Assert.IsNotNull(nrx);
            }
        }

        /// <summary>
        ///A test for ConfigSettingsService Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [Test()]
        public void ConfigSettingsServiceConstructorTest() {
            NameValueCollection settings = new NameValueCollection();
            ConfigSettingsService target = new ConfigSettingsService(settings);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GetSetting
        ///</summary>
        [Test()]
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
        [Test()]
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
        [Test()]
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
