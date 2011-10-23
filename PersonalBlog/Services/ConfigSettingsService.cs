using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Configuration;

namespace PersonalBlog.Services {
    public class ConfigSettingsService {

        private NameValueCollection settings = null;

        public ConfigSettingsService(NameValueCollection settings) {
            if (settings == null) throw new NullReferenceException(@"Cannot create the ConfigSettingsService with a null list of values");
            this.settings = settings;
        }

        public ConfigSettingsService() : this(System.Configuration.ConfigurationManager.AppSettings) {}

        public string GetSetting(string key, string defaultValue) {
            string val = settings[key];
            return string.IsNullOrEmpty(val) ? defaultValue : val;
        }

        public string GetSetting(string key) {
            return settings[key];
        }

        public int GetSetting(string key, int defaultValue) {
            string val = settings[key];
            if (!string.IsNullOrEmpty(val)) {
                int ret;
                if (int.TryParse(val, out ret)) return ret;
            }
            return defaultValue;
        }

        public string GetConnectionString(string connName) {
            ConnectionStringSettings c = ConfigurationManager.ConnectionStrings[connName];
            if (c != null) return c.ConnectionString;
            return string.Empty;
        }
    }
}