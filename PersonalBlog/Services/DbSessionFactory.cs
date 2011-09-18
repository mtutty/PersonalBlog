using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace PersonalBlog.Services {
    public class DbSessionFactory {

        private static FluentConfiguration cfg = null;

        public static ConfigSettingsService ConfigService { get; set; }
        public static string ConnectionName { get; set; }

        static DbSessionFactory() {
            ConfigService = new ConfigSettingsService();
            ConnectionName = ConfigService.GetSetting(@"ConnectionName", @"default");
        }

        public static string ConnectionStringFromConfig(string p) {
            ConnectionStringSettings cn = System.Configuration.ConfigurationManager.ConnectionStrings[p];
            if (cn != null) return cn.ConnectionString;
            return string.Empty;
        }

        /// <summary>
        /// Creates a new FluentConfiguration on demand.  This method depends on previous setting of the ConnectionName property
        /// </summary>
        /// <returns>A FluntConfiguration object, set up with database connection and object mappings.</returns>
        public static FluentConfiguration CreateConfig() {
            return Fluently.Configure()
              .Database(
                MySQLConfiguration.Standard.ConnectionString(ConnectionStringFromConfig(ConnectionName))
              )
              .Mappings(
                m => m.FluentMappings.AddFromAssemblyOf<DbSessionFactory>()
              );
        }

        /// <summary>
        /// Provides safe access to a FluentConfiguration.  Use this method instead of CreateConfig to avoid duplicate setup work.
        /// </summary>
        /// <returns>A FluntConfiguration object, set up with database connection and object mappings.</returns>
        public static FluentConfiguration Config {
            get {
                if (cfg == null) {
                    cfg = CreateConfig();
                }
                return cfg;
            }
            set {
                cfg = value;
            }
        }

        /// <summary>
        /// Builds a SessionFactory using the FluentConfig's database connection and object mappings
        /// </summary>
        public static ISessionFactory SessionFactory {
            get {
                var ret = Config.BuildSessionFactory();
                return ret;
            }
        }

        /// <summary>
        /// Opens a Session using the FluentConfig's database connection and object mappings
        /// </summary>
        public static ISession Session {
            get {
                var ret = SessionFactory.OpenSession();
                return ret;
            }
        }
    }
}