using App.Core.DataModels;

namespace App.Entities.ProductManagement
{
    public class ProductCategory : EntityBase, ICategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public bool? IsDisabled { get; set; }

        public virtual ProductCategory Parent { get; set; }
    }
}
