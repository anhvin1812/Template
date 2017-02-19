using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.ProductManagement
{
    public class ProductStatusSummary : DtoBase
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
