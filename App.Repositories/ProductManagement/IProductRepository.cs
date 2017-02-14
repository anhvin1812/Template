using System.Collections.Generic;
using App.Core.Repositories;
using App.Entities.ProductManagement;

namespace App.Repositories.ProductManagement
{
    public interface IProductRepository : IRepository
    {
        IEnumerable<Product> GetAll(int? page, int? pageSize, ref int? recordCount);
        Product GetById(int id);
        void Insert(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
