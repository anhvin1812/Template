using System.Collections.Generic;
using App.Entities.IdentityManagement;

namespace App.Entities.ProductManagement
{
    public class ProductStatus : EntityBase
    {
        public int Id { get; set; }

        public string Status { get; set; }
    }
}
