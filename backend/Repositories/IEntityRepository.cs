using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEntityRepository<T> where T : class, new()
    {
        T Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        ICollection<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, ICollection<T>> options = null,
            string includeProperties = null
        );

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
        );

    }
}
