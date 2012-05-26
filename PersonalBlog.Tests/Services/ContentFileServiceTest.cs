using PersonalBlog.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace PersonalBlog.Tests {

    /// <summary>
    ///This is a test class for ContentFileServiceTest and is intended
    ///to contain all ContentFileServiceTest Unit Tests
    ///</summary>
    [TestFixture()]
    public class ContentFileServiceTest {

        private ContentFileService GetContentFileService() {
            return new ContentFileService(new ConfigSettingsService());
        }

        private ContentFileService TempRootedCFS() {
            return this.CustomRootedCFS(System.IO.Path.GetTempPath());
        }

        private ContentFileService CustomRootedCFS(string root) {
            NameValueCollection customSettings = new NameValueCollection();
            customSettings.Add(@"content.rootfolder", root);
            return new ContentFileService(new ConfigSettingsService(customSettings));
        }

        /// <summary>
        ///A test for ContentFileService Constructor
        ///</summary>
        [Test()]
        public void ContentFileServiceConstructorTest() {
            ConfigSettingsService config = new ConfigSettingsService();
            ContentFileService target = new ContentFileService(config);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for ContentFileService Constructor
        ///</summary>
        [Test()]
        public void ContentFileServiceConstructorTestDefault() {
            ContentFileService target = GetContentFileService();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for ListFolders
        ///</summary>
        [Test()]
        public void ListFoldersTest() {
            ContentFileService target = GetContentFileService();
            string parentFolder = string.Empty; // TODO: Initialize to an appropriate value
            IList<string> actual;
            actual = target.ListFolders(parentFolder);
            Assert.IsNotNull(actual);
        }

        [Test()]
        public void ListFoldersWithoutPermissionsTest() {
            ContentFileService target = CustomRootedCFS(@"C:\windows\system32");
            string parentFolder = string.Empty; // TODO: Initialize to an appropriate value
            IList<string> actual;
            actual = target.ListFolders(parentFolder);
            Assert.IsNotNull(actual);
        }

        [Test()]
        public void ListFoldersInNonExistentRootTest() {
            ContentFileService target = CustomRootedCFS(@"c:\zzaadorothy\123");
            string parentFolder = string.Empty; // TODO: Initialize to an appropriate value
            IList<string> actual;
            actual = target.ListFolders(parentFolder);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for ListFolders
        ///</summary>
        [Test()]
        public void ListFoldersTestRoot() {
            ContentFileService target = this.TempRootedCFS();
            IList<string> actual;
            actual = target.ListFolders();
            Assert.AreNotEqual(0, actual.Count);
            Assert.IsFalse(actual.Contains(@"."));
            Assert.IsFalse(actual.Contains(@".."));
        }

        [Test()]
        public void RootFolderTest() {
            ContentFileService target = GetContentFileService();
            Assert.IsNotNull(target.RootFolder);

            target = this.TempRootedCFS();
            string expected = System.IO.Path.GetTempPath();
            Assert.AreEqual(expected, target.RootFolder);
        }
    }
}
