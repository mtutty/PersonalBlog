using PersonalBlog.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace PersonalBlog.Tests
{
    
    
    /// <summary>
    ///This is a test class for ContentFileServiceTest and is intended
    ///to contain all ContentFileServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ContentFileServiceTest {


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

        private ContentFileService TempRootedCFS() {
            return this.CustomRootedCFS(System.IO.Path.GetTempPath());
        }

        private ContentFileService CustomRootedCFS(string root) {
            NameValueCollection customSettings = new NameValueCollection();
            customSettings.Add(@"content.rootfolder", root);
            return new ContentFileService(new ConfigSettingsService(customSettings));
        }

        /// <summary>
        ///A test for ContentFileService Constructor
        ///</summary>
        [TestMethod()]
        public void ContentFileServiceConstructorTest() {
            ConfigSettingsService config = new ConfigSettingsService();
            ContentFileService target = new ContentFileService(config);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for ContentFileService Constructor
        ///</summary>
        [TestMethod()]
        public void ContentFileServiceConstructorTestDefault() {
            ContentFileService target = new ContentFileService();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for ListFolders
        ///</summary>
        [TestMethod()]
        public void ListFoldersTest() {
            ContentFileService target = new ContentFileService(); 
            string parentFolder = string.Empty; // TODO: Initialize to an appropriate value
            IList<string> actual;
            actual = target.ListFolders(parentFolder);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ListFoldersWithoutPermissionsTest() {
            ContentFileService target = CustomRootedCFS(@"C:\windows\system32");
            string parentFolder = string.Empty; // TODO: Initialize to an appropriate value
            IList<string> actual;
            actual = target.ListFolders(parentFolder);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ListFoldersInNonExistentRootTest() {
            ContentFileService target = CustomRootedCFS(@"c:\zzaadorothy\123");
            string parentFolder = string.Empty; // TODO: Initialize to an appropriate value
            IList<string> actual;
            actual = target.ListFolders(parentFolder);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for ListFolders
        ///</summary>
        [TestMethod()]
        public void ListFoldersTestRoot() {
            ContentFileService target = this.TempRootedCFS();
            IList<string> actual;
            actual = target.ListFolders();
            Assert.AreNotEqual(0, actual.Count);
            Assert.IsFalse(actual.Contains(@"."));
            Assert.IsFalse(actual.Contains(@".."));
        }

        [TestMethod]
        public void RootFolderTest() {
            ContentFileService target = new ContentFileService();
            Assert.IsNotNull(target.RootFolder);

            target = this.TempRootedCFS();
            string expected = System.IO.Path.GetTempPath();
            Assert.AreEqual(expected, target.RootFolder);
        }
    }
}
