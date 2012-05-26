﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace PersonalBlog.Models
{
    public partial class BlogPost
    {

        public const string StatusNew = @"N";
        public const string StatusPublished = @"P";
        public const string StatusArchived = @"A";
        public const string StatusSpecialPage = @"S";

        public const string ContentHtml = @"H";
        public const string ContentText = @"T";

        public BlogPost() {
            this.CreateDate = DateTime.Now;
            this.ContentType = ContentHtml;
            this.Comments = new List<Comment>();
            this.ViewCount = 0;
        }

        [BsonId(IdGenerator=typeof(StringObjectIdGenerator))]
        public virtual string Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public virtual global::System.String Title { get; set; }

        [StringLength(1, MinimumLength = 1)]
        public virtual global::System.String PublishStatus { get; set; }

        [StringLength(255)]
        public virtual global::System.String RewriteID { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public virtual global::System.String Author { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 1)]
        public virtual global::System.String ContentType { get; set; }

        [Required]
        [StringLength(65535, MinimumLength = 1)]
        [AllowHtml]
        public virtual global::System.String Content { get; set; }

        [Required]
        public virtual global::System.DateTime CreateDate { get; set; }

        public virtual global::System.Int32 ViewCount { get; set; }

        public virtual IList<Comment> Comments { get; set; }

    }
}