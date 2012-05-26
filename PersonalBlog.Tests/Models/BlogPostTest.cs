using PersonalBlog.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using PersonalBlog.Services;
using System.Collections.Specialized;
using System.Linq;

namespace PersonalBlog.Tests.Models {

    /// <summary>
    ///This is a test class for BlogPostTest and is intended
    ///to contain all BlogPostTest Unit Tests
    ///</summary>
    [TestFixture()]
    public class BlogPostTest {

        private ConfigSettingsService cfg = null;
        private PersonalBlogRepository repo = null;

        [TestFixtureSetUp]
        public void FixtureSetup() {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add(@"BlogPost.Archive.MaxRecords", @"12");
            nvc.Add(@"Mongo.Url", @"mongodb://localhost/personalblog");
            cfg = new ConfigSettingsService(nvc);
        }

        [SetUp]
        public void SetupRepository() {
            this.repo = new PersonalBlogRepository(this.cfg);
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
