using System.Collections.Generic;
using System.Web.Mvc;

namespace PersonalBlog.Models {
    public class BlogPostViewModel {
        public BlogPost BlogPost { get; set; }
        public IEnumerable<SelectListItem> PublishStatuses { get; set; }
    }
}