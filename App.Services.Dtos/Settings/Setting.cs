using System.ComponentModel.DataAnnotations;
using System.Web;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.Settings
{
    public class Option : DtoBase
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public HttpPostedFileBase Logo { get; set; }
    }

    public class TagDetail : DtoBase
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public bool IsDisabled { get; set; }
    }

    public class TagSummary : DtoBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDisabled { get; set; }
    }
}
