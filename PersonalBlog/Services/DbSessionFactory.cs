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

        private FluentConfiguration cfg = null;

        public ConfigSettingsService ConfigService { get; set; }
        public string ConnectionName { get; set; }

        public DbSessionFactory(ConfigSettingsService cfg) {
            ConfigService = cfg;
            ConnectionName = ConfigService.GetSetting(@"ConnectionName", @"default");
        }

        /// <summary>
        /// Creates a new FluentConfiguration on demand.  This method depends on previous setting of the ConnectionName property
        /// </summary>
        /// <returns>A FluntConfiguration object, set up with database connection and object mappings.</returns>
        public FluentConfiguration CreateConfig() {
            return Fluently.Configure()
              .Database(
                MySQLConfiguration.Standard.ConnectionString(ConfigService.GetConnectionString(ConnectionName))
              )
              .Mappings(
                m => m.FluentMappings.AddFromAssemblyOf<DbSessionFactory>()
              );
        }

        /// <summary>
        /// Provides safe access to a FluentConfiguration.  Use this method instead of CreateConfig to avoid duplicate setup work.
        /// </summary>
        /// <returns>A FluntConfiguration object, set up with database connection and object mappings.</returns>
        public FluentConfiguration Config {
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
        public ISessionFactory SessionFactory {
            get {
                var ret = Config.BuildSessionFactory();
                return ret;
            }
        }

        /// <summary>
        /// Opens a Session using the FluentConfig's database connection and object mappings
        /// </summary>
        public ISession Session {
            get {
                var ret = SessionFactory.OpenSession();
                return ret;
            }
        }
    }
}