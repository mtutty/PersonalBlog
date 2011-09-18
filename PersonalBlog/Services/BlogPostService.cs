using System.Collections.Generic;
using NHibernate;
using PersonalBlog.Models;
using NHibernate.Criterion;

namespace PersonalBlog.Services {
    public class BlogPostService {

        protected ISession db = null;
        protected ConfigSettingsService config = null;

        public BlogPostService(ISession db, ConfigSettingsService config) {
            this.db = db;
            this.config = config;
        }

        public ISession DBSession { get { return this.db; } }

        public IList<BlogPost> FrontPagePostings() {
            return this.db.CreateCriteria<BlogPost>()
                    .Add(Expression.Eq(@"PublishStatus", BlogPost.StatusPublished))
                    .AddOrder(Order.Desc(@"CreateDate"))
                    .SetMaxResults(this.config.GetSetting(@"BlogPost.FrontPage.MaxRecords", 15))
                    .List<BlogPost>();
        }

        public BlogPost FindByRewriteID(string RewriteID) {
            return this.db.CreateCriteria<BlogPost>()
                .Add(Expression.Eq(@"RewriteID", RewriteID))
                .UniqueResult<BlogPost>();
        }

        public IList<BlogPost> ArchivePostings() {
            return this.db.CreateCriteria<BlogPost>()
                .Add(Expression.Eq(@"PublishStatus", BlogPost.StatusArchived))
                .AddOrder(Order.Desc(@"CreateDate"))
                .SetMaxResults(this.config.GetSetting(@"BlogPost.Archive.MaxRecords", 15))
                .List<BlogPost>();
        }

        public IList<BlogPost> List(string PublishStatus) {
            return this.db.CreateCriteria<BlogPost>()
                .Add(Expression.Eq(@"PublishStatus", PublishStatus))
                .AddOrder(Order.Desc(@"CreateDate"))
                .List<BlogPost>();
        }

    }
}