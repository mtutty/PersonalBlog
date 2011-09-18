using System;
using System.Collections.Generic;
using System.Web;

using FluentNHibernate.Mapping;

namespace PersonalBlog.Models.Maps {
    public class BlogPostMap : ClassMap<BlogPost> {
        public BlogPostMap() {
            Id(x => x.Id);
            Map(x => x.Author).Length(255);
            Map(x => x.Content);
            Map(x => x.ContentType).Length(1);
            Map(x => x.CreateDate);
            Map(x => x.PublishStatus).Length(1);
            Map(x => x.RewriteID).Length(255);
            Map(x => x.Title).Length(255);
            Map(x => x.ViewCount);

            HasMany<Comment>(x => x.Comments)
                .LazyLoad()
                .Cascade.AllDeleteOrphan();
        }
    }
}