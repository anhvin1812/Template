using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using App.Core.Configuration;
using App.Core.Exceptions;
using App.Core.Repositories;
using App.Entities.IdentityManagement;
using App.Entities.ProductManagement;
using App.Infrastructure.File;
using App.Repositories.ProductManagement;
using App.Services.Dtos.ProductManagement;

namespace App.Services.Gallery
{
    public class GalleryService : ServiceBase, IGalleryService
    {
        #region Contructor
        private IGalleryRepository GalleryRepository { get; set; }

        public GalleryService(IUnitOfWork unitOfWork, IGalleryRepository galleryRepository)
            : base(unitOfWork, new IRepository[] { galleryRepository }, new IService[] { })
        {
            GalleryRepository = galleryRepository;
        }

        #endregion

        #region Public Methods

        public void Delete(int id)
        {
            var entity = GalleryRepository.GetById(id);
            if (entity == null)
                throw new DataNotFoundException();

            // delete files from server
            GalleryHelper.DeleteGallery(entity.Image, entity.Thumbnail);

            GalleryRepository.Delete(id);
            Save();
        }


        #endregion

        #region Private Methods


        #endregion


        #region Dispose
        private bool _disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (!this._disposed)
            {
                if (isDisposing)
                {
                    GalleryRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
