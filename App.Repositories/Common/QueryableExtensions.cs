using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Common
{
    internal static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> collection, int page, int pageSize)
        {
            collection = collection.Skip((page > 0 ? page - 1 : 0) * pageSize).Take(pageSize);
            return collection;
        }
    }
}
