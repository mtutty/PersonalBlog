using PersonalBlog.Services;
using NUnit.Framework;
using System;
using NHibernate;
using PersonalBlog.Models;
using System.Collections.Generic;

namespace PersonalBlog.Tests {
    /// <summary>
    ///This is a test class for BlogPostServiceTest and is intended
    ///to contain all BlogPostServiceTest Unit Tests
    ///</summary>
    [TestFixture()]
    public class BlogPostServiceTest : InjectedUnitTest {

        /// <summary>
        ///A test for BlogPostService Constructor
        ///</summary>
        [Test()]
        public void BlogPostServiceConstructorTest() {
            Assert.IsNotNull(Get<BlogPostService>());
        }

        /// <summary>
        ///A test for ArchivePostings
        ///</summary>
        [Test()]
        public void ArchivePostingsTest() {
            Assert.IsNotNull(Get<BlogPostService>().ArchivePostings());
        }

        /// <summary>
        ///A test for FindByRewriteID
        ///</summary>
        [Test()]
        public void FindByRewriteIDTest() {
            string RewriteID = @"nonsense-posting-rewrite-xx23";
            Assert.IsNull(Get<BlogPostService>().FindByRewriteID(RewriteID));
        }

        /// <summary>
        ///A test for FrontPagePostings
        ///</summary>
        [Test()]
        public void FrontPagePostingsTest() {
            Assert.IsNotNull(Get<BlogPostService>().FrontPagePostings());
        }
    }
}
