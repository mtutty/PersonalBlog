using System.Collections.Generic;
using PersonalBlog.Models;
using MongoDB;
using FluentMongo.Linq;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace PersonalBlog.Services {
    public class PersonalBlogRepository {

        protected ConfigSettingsService cfg;
        protected MongoDB.Driver.MongoDatabase db;

        public PersonalBlogRepository() : this(new ConfigSettingsService()) { }

        public PersonalBlogRepository(ConfigSettingsService config) {
            this.cfg = config;
            this.db = MongoDB.Driver.MongoDatabase.Create(this.cfg.GetSetting(@"Mongo.Url", @"mongodb://localhost/personalblog"));
        }

        public MongoDB.Driver.MongoDatabase Database {
            get {
                return this.db;
            }
        }

        public IQueryable<BlogPost> BlogPosts {
            get {
                return db.GetCollection<BlogPost>(@"BlogPost").AsQueryable<BlogPost>();
            }
        }

        public void Save(BlogPost post) {
            var result = db.GetCollection<BlogPost>(@"BlogPost").Save(post);
            AssertResult(result);
        }

        public void Delete(string id) {
            var result = db.GetCollection<BlogPost>(@"BlogPost").Remove(Query.EQ("_id", id));
            AssertResult(result);
        }

        private static void AssertResult(MongoDB.Driver.SafeModeResult result) {
            if (result == null)
                return;

            if (result.Ok == false) {
                throw new System.Exception(result.LastErrorMessage);
            }
        }

    }
}