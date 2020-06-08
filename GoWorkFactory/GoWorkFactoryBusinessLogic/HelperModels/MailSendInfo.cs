using System.Collections.Generic;

namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class MailSendInfo
    {
        public string MailAddress { get; set; }

        public string Subject { get; set; }
        public string Text { get; set; }
        public bool IsBodyHtml { get; set; }

        public List<MailAttachment> Attachments { get; set; }
    }
}
