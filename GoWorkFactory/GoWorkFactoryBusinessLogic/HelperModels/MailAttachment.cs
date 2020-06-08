using System.IO;

namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class MailAttachment
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public Stream FileData { get; set; }
    }
}
