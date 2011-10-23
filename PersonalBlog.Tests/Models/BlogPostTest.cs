using PersonalBlog.Models;
using NUnit.Framework;
using System;
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
    [TestFixture()]
    public class BlogPostTest : InjectedUnitTest {

        [Test()]
        public void SessionFactoryWorks() {
            Assert.IsNotNull(Get<DbSessionFactory>().SessionFactory);
            Assert.IsNotNull(Get<DbSessionFactory>().Session);
        }

        [Test()]
        public void CreateDBWorks() {
            Get<DbSessionFactory>().CreateConfig()
                .ExposeConfiguration(cfg => {
                    try {
                        new SchemaExport(cfg)
                            .Drop(false, true);
                    } catch { /* ignore */ }
                    new SchemaExport(cfg)
                        .Create(false, true);
                });
        }

        [Test()]
        public void TestRepositoryWorks() {
            var db = Get<DbSessionFactory>().Session;
            Assert.IsNotNull(db);
            Assert.IsNotNull(db.CreateCriteria<BlogPost>().List<BlogPost>());
        }

        [Test()]
        public void TestBlogPostLifeCycle() {
            var db = Get<DbSessionFactory>().Session;
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
