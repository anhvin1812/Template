using System.Collections.Generic;
using System.Linq;
using App.Core.DataModels;
using App.Entities;

namespace App.Repositories.Common
{
    internal static class EntityExtensions
    {
        public static List<int> GetHereditaryIds(this IQueryable<ICategory> queryable, int rootId )
        {
            var hereditaryIds = new List<int> { rootId};

            var chirendIds = queryable.Where(t => t.ParentId == rootId).Select(t => t.Id);
            if (!chirendIds.Any()) return hereditaryIds;

            foreach (int childId in chirendIds)
            {
                hereditaryIds.AddRange(queryable.GetHereditaryIds(childId));
            }

            return hereditaryIds;
        }

    }
}
