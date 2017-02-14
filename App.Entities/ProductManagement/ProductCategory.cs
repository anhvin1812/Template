using System.Collections.Generic;
using System.Data.Entity.Migrations.Builders;

namespace App.Entities.ProductManagement
{
    public class ProductCategory : EntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ParentId { get; set; }
    }
}
