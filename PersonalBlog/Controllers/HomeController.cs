using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PersonalBlog.Models;
using NHibernate;
using PersonalBlog.Services;

namespace PersonalBlog.Controllers
{
    public class HomeController : Controller
    {
        //private PersonalBlogModelContainer db = new PersonalBlogModelContainer();
        protected ISession db;
        protected ConfigSettingsService settings;
        protected BlogPostService blogService;

        public HomeController(ISession db, ConfigSettingsService settings, BlogPostService blogService) {
            this.db = db;
            this.settings = settings;
            this.blogService = blogService;
        }

        public HomeController() {
            this.db = DbSessionFactory.Session;
            this.settings = new ConfigSettingsService();
            this.blogService = new BlogPostService(this.db, settings);
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
