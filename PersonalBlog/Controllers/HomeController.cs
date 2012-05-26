using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PersonalBlog.Models;
using PersonalBlog.Services;

namespace PersonalBlog.Controllers
{
    public class HomeController : BaseController
    {
        protected BlogPostService blogService;

        public HomeController() : base() {
            this.blogService = base.GetBlogPostService();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            ViewBag.BlogPosts = this.blogService.FrontPagePostings();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";
            ViewBag.AboutPost = this.blogService.FindByRewriteID(@"about");
            return View();
        }

        public ActionResult Archive()
        {
            ViewBag.Title = "Archives";
            ViewBag.BlogPosts = this.blogService.ArchivePostings();

            return View(@"Index");
        }
    }
}
