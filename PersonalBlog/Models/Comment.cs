using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalBlog.Models {
    public class Comment {
        [Required]
        public virtual global::System.Int32 Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public virtual global::System.String Title { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public virtual global::System.String Author { get; set; }

        [Required]
        public virtual global::System.DateTime CreateDate { get; set; }

        [Required]
        [StringLength(65535, MinimumLength = 1)]
        public virtual global::System.String Content { get; set; }

    }
}