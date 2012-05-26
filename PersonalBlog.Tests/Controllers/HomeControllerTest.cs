using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using PersonalBlog;
using PersonalBlog.Controllers;
using PersonalBlog.Tests;
using PersonalBlog.Services;

namespace PersonalBlog.Tests.Controllers
{
    [TestFixture()]
    public class HomeControllerTest {

        private HomeController GetHomeController() {
            return new HomeController(
                new BlogPostService(new ConfigSettingsService())
                );
        }

        [Test(), Ignore("Homogeneous AppDomain issue")]
        public void Index()
        {
            // Arrange
            HomeController controller = GetHomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Home", result.ViewBag.Title);

            Assert.IsNotNull(result.ViewBag.BlogPosts);
        }

        [Test(), Ignore("Homogeneous AppDomain issue")]
        public void About()
        {
            // Arrange
            HomeController controller = GetHomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
