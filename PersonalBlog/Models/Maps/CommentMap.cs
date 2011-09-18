using System;
using System.Collections.Generic;
using System.Web;

using FluentNHibernate.Mapping;

namespace PersonalBlog.Models.Maps {
    public class CommentMap : ClassMap<Comment> {
        public CommentMap() {
            Id(x => x.Id);
            Map(x => x.Author).Length(255);
            Map(x => x.Content);
            Map(x => x.CreateDate);
            Map(x => x.Title).Length(255);
            HasOne<BlogPost>(x => x.Post);
        }
    }
}