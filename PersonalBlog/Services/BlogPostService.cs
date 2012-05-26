using System.Collections.Generic;
using PersonalBlog.Models;
using MongoDB;
using FluentMongo.Linq;
using System.Linq;
using MongoDB.Bson;
using System;

namespace PersonalBlog.Services {
    public class BlogPostService : PersonalBlogRepository {

        public BlogPostService() : base() { }
        public BlogPostService(ConfigSettingsService config) : base(config) { }

        public IList<BlogPost> FrontPagePostings() {
            var q = this.BlogPosts.Where(x => x.PublishStatus == BlogPost.StatusPublished)
                                  .OrderByDescending(x => x.CreateDate)
                                  .Take(this.cfg.GetSetting(@"BlogPost.Archive.MaxRecords", 15));
            return q.ToList();
        }

        public BlogPost FindByRewriteID(string RewriteID) {
            return this.BlogPosts
                .Where(x => x.RewriteID == RewriteID)
                .FirstOrDefault();
        }

        public BlogPost FindByID(string id) {
            return this.BlogPosts
                       .Where(x => x.Id == id)
                       .FirstOrDefault();
        }

        public IList<BlogPost> ArchivePostings() {
            var q = this.BlogPosts
                .Where(x => x.PublishStatus == BlogPost.StatusArchived)
                .OrderByDescending(x => x.CreateDate)
                .Take(this.cfg.GetSetting(@"BlogPost.Archive.MaxRecords", 15));

            return q.ToList();
        }

        public IList<BlogPost> List(string PublishStatus) {
            var q = this.BlogPosts
                .Where(x => x.PublishStatus == PublishStatus)
                .OrderByDescending(x => x.CreateDate);

            return q.ToList();
        }
    }
}