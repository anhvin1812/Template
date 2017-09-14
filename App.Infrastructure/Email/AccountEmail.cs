using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using App.Core.Configuration;
using App.Core.Email;

namespace App.Infrastructure.Email
{
    public class ConfirmEmailMail : Mail
    {
        private string ViewPath => "~/EmailTemplates/ConfirmEmail.cshtml";

        public ConfirmEmailMail(string emailTo, object viewModel)
            : base("Please confirm your email address", emailTo)
        {
            Body = GenerateBodyFromViewRazor(viewModel, ViewPath);
        }
    }
}
