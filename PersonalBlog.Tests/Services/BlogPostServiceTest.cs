using PersonalBlog.Services;
using NUnit.Framework;
using System;
using PersonalBlog.Models;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace PersonalBlog.Tests {
    /// <summary>
    ///This is a test class for BlogPostServiceTest and is intended
    ///to contain all BlogPostServiceTest Unit Tests
    ///</summary>
    [TestFixture()]
    public class BlogPostServiceTest {

        private BlogPostService svc = null;

        private ConfigSettingsService cfg = null;

        [TestFixtureSetUp]
        public void FixtureSetup() {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add(@"BlogPost.Archive.MaxRecords", @"12");
            nvc.Add(@"Mongo.Url", @"mongodb://localhost/personalblog-unit");
            cfg = new ConfigSettingsService(nvc);
        }

        [SetUp]
        public void Setup() {
            this.svc = new BlogPostService(this.cfg);
        }

        /// <summary>
        ///A test for BlogPostService Constructor
        ///</summary>
        [Test()]
        public void BlogPostServiceConstructorTest() {
            Assert.IsNotNull(this.svc);
        }

        /// <summary>
        ///A test for ArchivePostings
        ///</summary>
        [Test()]
        public void ArchivePostingsTest() {
            Assert.IsNotNull(this.svc.ArchivePostings());
        }

        /// <summary>
        ///A test for FindByRewriteID
        ///</summary>
        [Test()]
        public void FindByRewriteIDTest() {
            string RewriteID = @"nonsense-posting-rewrite-xx23";
            Assert.IsNull(this.svc.FindByRewriteID(RewriteID));
        }

        /// <summary>
        ///A test for FrontPagePostings
        ///</summary>
        [Test()]
        public void FrontPagePostingsTest() {
            Assert.IsNotNull(this.svc.FrontPagePostings());
        }

        [Test()]
        public void TestRepositoryWorks() {
            Assert.GreaterOrEqual(this.svc.BlogPosts.Count(), 0);
        }

        [Test()]
        public void TestBlogPostLifeCycle() {
            var post = new BlogPost() {
                Author = @"Unit Test Author",
                CreateDate = DateTime.Now,
                Title = @"Unit Test Post Title",
                PublishStatus = BlogPost.StatusNew,
                ContentType = BlogPost.ContentText,
                RewriteID = @"unit_test_rewrite",
                Content = @"Here is the plain-text content for unit test blog posting"
            };

            this.svc.Save(post);
            Assert.AreNotEqual(string.Empty, post.Id);

            var reloaded = this.svc.FindByID(post.Id);

            Assert.AreEqual(post.Title, reloaded.Title);

            post.RewriteID = @"other_rewrite_id";
            this.svc.Save(post);

            reloaded = this.svc.FindByID(post.Id);
            Assert.AreEqual(post.RewriteID, reloaded.RewriteID);

            // Add comments to this blog posting
            for (int i = 0; i < 5; i++) {
                post.Comments.Add(new Comment() {
                    Author = string.Format(@"Author {0}", i),
                    Content = string.Format(@"This is comment #{0}!", i),
                    CreateDate = DateTime.Now.AddHours(-1 * i),
                    Title = string.Format(@"Comment #{0}", i)
                });
            }
            this.svc.Save(post);

            reloaded = this.svc.FindByID(post.Id);
            Assert.AreEqual(5, reloaded.Comments.Count);

            for (int i = 0; i < 5; i++) {
                Assert.AreEqual(post.Comments[i].Author, reloaded.Comments[i].Author);
                Assert.AreEqual(post.Comments[i].Content, reloaded.Comments[i].Content);
                Assert.AreEqual(post.Comments[i].Id, reloaded.Comments[i].Id);

                // Funky date behavior - even comparing ToUniversalTime() directly fails, 
                // have to use a string representation to eliminate the crazy precision 
                // that's apparently making them appear different.
                string expected = post.Comments[i].CreateDate.ToUniversalTime().ToString(@"u");
                string actual = reloaded.Comments[i].CreateDate.ToString(@"u");
                Assert.AreEqual(expected, actual);

                Assert.AreEqual(post.Comments[i].Title, reloaded.Comments[i].Title);
            }

            this.svc.Delete(post.Id);
        }

    }
}
