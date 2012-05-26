using System.Collections.Generic;
using PersonalBlog.Models;
using MongoDB;
using FluentMongo.Linq;
using System.Linq;
using MongoDB.Bson;
using System;

namespace PersonalBlog.Services {
    public class BlogPostService : IDisposable {

        protected ConfigSettingsService config = null;
        protected PersonalBlogRepository repo = null;

        public BlogPostService(ConfigSettingsService config) {
            this.repo = new PersonalBlogRepository(config);
            this.config = config;
        }

        public IList<BlogPost> FrontPagePostings() {
            var q = this.repo.BlogPosts.Where(x => x.PublishStatus == BlogPost.StatusPublished)
                                  .OrderByDescending(x => x.CreateDate)
                                  .Take(this.config.GetSetting(@"BlogPost.Archive.MaxRecords", 15));
            return q.ToList();
        }

        public BlogPost FindByRewriteID(string RewriteID) {
            return this.repo.BlogPosts
                .Where(x => x.RewriteID == RewriteID)
                .FirstOrDefault();
        }

        public BlogPost FindByID(string id) {
            var docId = ObjectId.Parse(id);
            return this.repo.BlogPosts
                       .Where(x => x.Id == docId)
                       .FirstOrDefault();
        }

        public BlogPost Save(BlogPost post) {
            this.repo.Save(post);
            return post;
        }

        public void Delete(string id) {
            var docId = ObjectId.Parse(id);
            this.repo.Delete(docId);
        }

        public IList<BlogPost> ArchivePostings() {
            var q = this.repo.BlogPosts
                .Where(x => x.PublishStatus == BlogPost.StatusArchived)
                .OrderByDescending(x => x.CreateDate)
                .Take(this.config.GetSetting(@"BlogPost.Archive.MaxRecords", 15));

            return q.ToList();
        }

        public IList<BlogPost> List(string PublishStatus) {
            var q = this.repo.BlogPosts
                .Where(x => x.PublishStatus == PublishStatus)
                .OrderByDescending(x => x.CreateDate);

            return q.ToList();
        }


        public void Dispose() {
            // TODO: Do we need this?
        }
    }
}