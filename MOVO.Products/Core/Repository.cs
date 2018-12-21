using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;

namespace MOVO.Products.Core {
    public class Repository<T> : IRepository<T> {
        private readonly ISession session;

        public Repository(ISession session) {
            this.session = session;
        }

        public T Get(object id) {
            T result;
            using (var transaction = this.session.BeginTransaction()) {
                result = this.session.Get<T>(id);
                transaction.Commit();
            }
            return result;
        }

        public void Update(T obj) {
            using (var transaction = this.session.BeginTransaction()) {
                this.session.Update(obj);
                transaction.Commit();
            }
        }

        public void Save(T obj) {
            using (var transaction = this.session.BeginTransaction()) {
                this.session.Save(obj);
                transaction.Commit();
            }
        }

        public IEnumerable<T> GetAll() {
            IEnumerable<T> allItems;
            using (var transaction = this.session.BeginTransaction()) {
                allItems = this.session.Query<T>();
                transaction.Commit();
            }
            return allItems;
        }

        public IQueryable<T> Filter<T>(Expression<Func<T, bool>> func) where T : class {
            IQueryable<T> filteredItems;
            using (var transaction = this.session.BeginTransaction()) {
                filteredItems = this.session.Query<T>().Where(func);
                transaction.Commit();
            }

            return filteredItems;
        }
    }
}