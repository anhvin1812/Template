using System.Collections.Generic;
using App.Entities.ProductManagement;
using App.Services.Dtos.Gallery;
using App.Services.Dtos.ProductManagement;

namespace App.Services.ProductManagement
{
    public interface IProductService : IService
    {
        IEnumerable<ProductSummary> GetAll(string keyword, int? categoryId, int? page, int? pageSize, ref int? recordCount);
        IEnumerable<ProductSummary> GetRelatedProducts(int productId, int categoryId, int? maxRecords = null);
        ProductDetail GetById(int id);
        ProductUpdateEntry GetProductForEditing(int id);
        void Insert(ProductEntry entry);
        void Update(int id, ProductUpdateEntry entry);

        #region Product Status
        IEnumerable<ProductStatusSummary> GetAllStatus();
        #endregion


        #region Products Gallery

        IEnumerable<GallerySummary> GetGalleryByProductId(int id);
        void DeleteProductGallery(int productId, int galleryId);

        #endregion
    }
}
