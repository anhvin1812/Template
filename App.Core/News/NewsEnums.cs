
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.News
{
    public enum NewsStatus
    {
        Private = 1,
        Public = 2,
        Draft = 3,
        [Display(Name = "Pending Validation")]
        PendingValidation = 4
    }

    public enum MediaType
    {
        Standard = 1,
        Photo = 2,
        Video = 3
    }

    public enum LayoutType
    {
        OneCategory = 1,
        TwoCategory = 2,
        HorizontalSlider = 3,
        PhotoSlider = 4,
    }
}
