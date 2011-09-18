using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PersonalBlog.Services {
    public class ContentFileService {

        private ConfigSettingsService config = new ConfigSettingsService();

        public ContentFileService() { }

        public ContentFileService(ConfigSettingsService config) {
            this.config = config;
        }

        public string RootFolder {
            get {
                return Path.GetFullPath(config.GetSetting(@"content.rootfolder", AppDomain.CurrentDomain.BaseDirectory));
            }
        }

        public IList<string> ListFolders() {
            return this.ListFolders(null);
        }

        public IList<string> ListFolders(string parentFolder) {
            string path = this.RootFolder;
            if (!string.IsNullOrEmpty(parentFolder)) {
                path = Path.Combine(path, parentFolder);
            }
            IList<String> ret = new List<string>();
            DirectoryInfo di = new DirectoryInfo(path);
            if (Directory.Exists(path)) {
                foreach (string dir in Directory.GetDirectories(path)) {
                    if (@"..".Equals(dir)) continue;
                    if (@".".Equals(dir)) continue;
                    ret.Add(dir);
                }
            }
            return ret;
        }

    }
}