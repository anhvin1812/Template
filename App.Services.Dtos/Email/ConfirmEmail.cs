using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.Email
{
    public class ConfirmEmail : DtoBase
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
}
