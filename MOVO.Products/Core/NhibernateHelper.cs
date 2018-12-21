using System;
using System.IO;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using FluentNHibernate.Mapping.Providers;
using MOVO.Products.Contracts.Products;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace MOVO.Products.Core {
    public class NhibernateHelper {
        private static ISessionFactory sessionFactory;

        private static readonly string dbFileName = $"{Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"), "dbFile.sqllite")}";

        private static ISessionFactory SessionFactory => sessionFactory ?? (sessionFactory = CreateSessionFactory());

        public static ISession OpenSession() {
            return SessionFactory.OpenSession();
        }

        private static ISessionFactory CreateSessionFactory() {
            if (!File.Exists(dbFileName)) {
                File.Create(dbFileName);
            }

            return Fluently.Configure()
                           .Database(SQLiteConfiguration.Standard.UsingFile(dbFileName))
                           .Mappings(m => { m.FluentMappings.AddFromNamespaceOf<Product>(); })
                           .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                           .BuildSessionFactory();
        }
    }

    public static class FluentNHibernateExtensions {
        public static FluentMappingsContainer AddFromNamespaceOf<T>(this FluentMappingsContainer fmc) {
            var ns = typeof(T).Namespace;
            var types = typeof(T).Assembly.GetExportedTypes()
                                 .Where(t => t.Namespace == ns)
                                 .Where(x => IsMappingOf<IMappingProvider>(x) ||
                                             IsMappingOf<IIndeterminateSubclassMappingProvider>(x) ||
                                             IsMappingOf<IExternalComponentMappingProvider>(x) ||
                                             IsMappingOf<IFilterDefinition>(x));

            foreach (var t in types) {
                fmc.Add(t);
            }

            return fmc;
        }

        private static bool IsMappingOf<T>(Type type) {
            return !type.IsGenericType && typeof(T).IsAssignableFrom(type);
        }
    }
}