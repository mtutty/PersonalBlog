using PersonalBlog.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using NHibernate;
using PersonalBlog.Models;
using System.Collections.Generic;

namespace PersonalBlog.Tests
{
    
    
    /// <summary>
    ///This is a test class for BlogPostServiceTest and is intended
    ///to contain all BlogPostServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BlogPostServiceTest {


        private TestContext testContextInstance;
        private BlogPostService blogService = null;

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
        //public static void MyClassInitialize(TestContext testContext) {
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

        private BlogPostService BlogService {
            get {
                if (this.blogService == null) {
                    ISession s = DbSessionFactory.Session;
                    this.blogService = new BlogPostService(s, new ConfigSettingsService());
                }
                return this.blogService;
            }
        }

        /// <summary>
        ///A test for BlogPostService Constructor
        ///</summary>
        [TestMethod()]
        public void BlogPostServiceConstructorTest() {
            ISession db = DbSessionFactory.Session;
            ConfigSettingsService config = new ConfigSettingsService();
            BlogPostService target = new BlogPostService(db, config);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for ArchivePostings
        ///</summary>
        [TestMethod()]
        public void ArchivePostingsTest() {
            Assert.IsNotNull(BlogService.ArchivePostings());
        }

        /// <summary>
        ///A test for FindByRewriteID
        ///</summary>
        [TestMethod()]
        public void FindByRewriteIDTest() {
            string RewriteID = @"nonsense-posting-rewrite-xx23";
            Assert.IsNull(BlogService.FindByRewriteID(RewriteID));
        }

        /// <summary>
        ///A test for FrontPagePostings
        ///</summary>
        [TestMethod()]
        public void FrontPagePostingsTest() {
            Assert.IsNotNull(BlogService.FrontPagePostings());
        }
    }
}
