using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MOVO.Products.Core {
    //more methods not needed for this example
    public interface IRepository<T> {
        T Get(object id);
        void Update(T obj);
        void Save(T obj);
        IEnumerable<T> GetAll();
        IQueryable<T> Filter<T>(Expression<Func<T, bool>> func) where T : class;
    }
}