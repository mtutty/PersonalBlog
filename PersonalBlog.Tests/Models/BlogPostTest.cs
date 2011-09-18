using PersonalBlog.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Configuration;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Testing;
using System.Collections;
using PersonalBlog.Services;

namespace PersonalBlog.Tests.Models {

    /// <summary>
    ///This is a test class for BlogPostTest and is intended
    ///to contain all BlogPostTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BlogPostTest {
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

        [TestMethod]
        public void SessionFactoryWorks() {
            Assert.IsNotNull(DbSessionFactory.SessionFactory);
            Assert.IsNotNull(DbSessionFactory.Session);
        }

        [TestMethod]
        public void CreateDBWorks() {
            DbSessionFactory.CreateConfig()
                .ExposeConfiguration(cfg => {
                    try {
                        new SchemaExport(cfg)
                            .Drop(false, true);
                    } catch { /* ignore */ }
                    new SchemaExport(cfg)
                        .Create(false, true);
                });
        }

        [TestMethod]
        public void TestRepositoryWorks() {
            var db = DbSessionFactory.Session;
            Assert.IsNotNull(db);
            Assert.IsNotNull(db.CreateCriteria<BlogPost>().List<BlogPost>());
        }

        [TestMethod]
        public void TestBlogPostLifeCycle() {
            var db = DbSessionFactory.Session;
            Assert.IsNotNull(db);

            var post = new BlogPost() {
                Author = @"Unit Test Author",
                CreateDate = DateTime.Now,
                Title = @"Unit Test Post Title",
                PublishStatus = BlogPost.StatusNew,
                ContentType = BlogPost.ContentText,
                RewriteID = @"unit_test_rewrite",
                Content = @"Here is the plain-text content for unit test blog posting"
            };

            db.Save(post);
            Assert.AreNotEqual(0, post.Id);

            var reloaded = db.Get<BlogPost>(post.Id);

            Assert.AreEqual(post.Title, reloaded.Title);

            post.RewriteID = @"other_rewrite_id";
            db.Update(post);

            Assert.AreEqual(@"other_rewrite_id", reloaded.RewriteID);

            reloaded = db.Get<BlogPost>(post.Id);
            Assert.AreEqual(post.RewriteID, reloaded.RewriteID);

            // Add comments to this blog posting
            for (int i = 0; i < 5; i++) {
                post.Comments.Add(new Comment() {
                    Author = string.Format(@"Author {0}", i),
                    Content = string.Format(@"This is comment #{0}!", i),
                    CreateDate = DateTime.Now.AddHours(-1 * i),
                    Post = post,
                    Title = string.Format(@"Comment #{0}", i)
                });
            }
            db.SaveOrUpdate(post);

            reloaded = db.Get<BlogPost>(post.Id);
            Assert.AreEqual(5, reloaded.Comments.Count);

            for (int i = 0; i < 5; i++) {
                Assert.AreEqual(post.Comments[i].Author, reloaded.Comments[i].Author);
                Assert.AreEqual(post.Comments[i].Content, reloaded.Comments[i].Content);
                Assert.AreEqual(post.Comments[i].Id, reloaded.Comments[i].Id);
                Assert.AreEqual(post.Comments[i].CreateDate, reloaded.Comments[i].CreateDate);
                Assert.AreEqual(post.Comments[i].Title, reloaded.Comments[i].Title);
            }

            db.Delete(post);
        }
    }

    public class DateComparer : IEqualityComparer {
        bool IEqualityComparer.Equals(object x, object y) {
            if (x == null || y == null) {
                return false;
            }
            if (x is DateTime && y is DateTime) {
                return ((DateTime)x).Ticks == ((DateTime)y).Ticks;
            }
            return x.Equals(y);
        }

        int IEqualityComparer.GetHashCode(object obj) {
            throw new NotImplementedException();
        }
    }
}
