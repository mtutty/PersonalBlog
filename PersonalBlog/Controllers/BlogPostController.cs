using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalBlog.Models;
using PersonalBlog.Services;
using NHibernate;

namespace PersonalBlog.Controllers
{ 
    [Authorize()]
    public class BlogPostController : Controller
    {
        private BlogPostService bps = null;

        public BlogPostController(BlogPostService bps) {
            this.bps = bps;
        }

        //
        // GET: /BlogPost/

        public ViewResult Index(string status) {
            ViewBag.Title = "Postings Admin";
            ViewBag.ShowRightBlocks = false;
            status = SessionValue(@"BlogPostController.filter.status", status, BlogPost.StatusPublished);
            ViewBag.status = this.GetPublishStatusList(status);
            ViewBag.Postings = this.bps.List(status);
            return View();
        }

        private IEnumerable<SelectListItem> GetPublishStatusList(string currentStatus = BlogPost.StatusNew)
        {
            return new SelectListItem[]
                {
                    this.CreateListItem(currentStatus, BlogPost.StatusPublished, @"Published"),
                    this.CreateListItem(currentStatus, BlogPost.StatusNew, @"New"),
                    this.CreateListItem(currentStatus, BlogPost.StatusArchived, @"Archived"),
                    this.CreateListItem(currentStatus, BlogPost.StatusSpecialPage, @"Special Pages")
                };
        }

        private SelectListItem CreateListItem(string selectedStatus, string status, string title)
        {
            return new SelectListItem() { Selected = (selectedStatus == status), Text = title, Value = status };
        }

        //
        // GET: /BlogPost/Details/5

        public ViewResult Details(int id)
        {
            ViewBag.ShowRightBlocks = false;
            BlogPost blogpost = this.bps.DBSession.Load<BlogPost>(id);
            return View(blogpost);
        }

        //
        // GET: /BlogPost/Create

        public ActionResult Create()
        {
            ViewBag.Title = @"New Blog Post";
            ViewBag.ShowRightBlocks = false;
            var model = new BlogPostViewModel()
                {
                    BlogPost = new BlogPost()
                        {
                            PublishStatus = BlogPost.StatusNew,
                            Author = User.Identity.Name,
                            ContentType = @"H",
                            CreateDate = DateTime.Now
                        },
                    PublishStatuses = this.GetPublishStatusList()
                };
            return View(model);
        } 

        //
        // POST: /BlogPost/Create

        [HttpPost]
        public ActionResult Create(BlogPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                this.bps.DBSession.Save(model.BlogPost);
                
                return RedirectToAction("Index");  
            }

            model.PublishStatuses = this.GetPublishStatusList();
            return View(model);
        }
        
        //
        // GET: /BlogPost/Edit/5
 
        public ActionResult Edit(int id)
        {
            var model = new BlogPostViewModel()
                {
                    BlogPost = this.bps.DBSession.Load<BlogPost>(id),
                    PublishStatuses = this.GetPublishStatusList()
                };
            return View(model);
        }

        //
        // POST: /BlogPost/Edit/5

        [HttpPost]
        public ActionResult Edit(BlogPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                this.bps.DBSession.Merge(model.BlogPost);
                return RedirectToAction("Index");
            }

            model.PublishStatuses = this.GetPublishStatusList();
            return View(model);
        }

        //
        // GET: /BlogPost/Delete/5
 
        public ActionResult Delete(int id)
        {
            BlogPost model = this.bps.DBSession.Load<BlogPost>(id);
            return View(model);
        }

        //
        // POST: /BlogPost/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost model = this.bps.DBSession.Load<BlogPost>(id);
            this.bps.DBSession.Delete(model);
            return RedirectToAction("Index");
        }

        protected string SessionValue(string key, string inputValue, string defaultValue) {
            string ret;
            if (string.IsNullOrEmpty(inputValue)) {
                ret = Session[key] as string;
                if (string.IsNullOrEmpty(ret)) {
                    ret = defaultValue;
                }
            } else {
                ret = inputValue;
            }
            if (ret != defaultValue) {
                Session[key] = ret;
            }
            return ret;
        }

        protected override void Dispose(bool disposing)
        {
            if (this.bps != null)
                this.bps.DBSession.Flush();
            base.Dispose(disposing);
        }
    }
}