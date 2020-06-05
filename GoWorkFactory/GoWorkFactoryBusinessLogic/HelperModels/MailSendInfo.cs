using System;
using System.Collections.Generic;
using System.Text;

namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class MailSendInfo
    {
        public string MailAddress { get; set; }

        public string Subject { get; set; }
        public string Text { get; set; }

        public List<MailAttachment> Attachments { get; set; }
    }
}
