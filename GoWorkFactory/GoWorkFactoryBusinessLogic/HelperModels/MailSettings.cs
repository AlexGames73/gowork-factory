using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class MailSettings
    {
        public string SmtpClientHost { get; set; }
        public int SmtpClientPort { get; set; }
        public string MailLogin { get; set; }
        public string MailPassword { get; set; }
    }
}
