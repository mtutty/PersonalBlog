using System.Web.Mvc;
using PersonalBlog.Services;

namespace PersonalBlog.Controllers {
    public class BaseController : Controller {

        protected ConfigSettingsService config = null;

        public BaseController() {
            this.config = new ConfigSettingsService();
        }

        protected BlogPostService GetBlogPostService() {
            return new BlogPostService(this.config);
        }
    }
}