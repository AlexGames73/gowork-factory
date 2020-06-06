using GoWorkFactoryBusinessLogic.HelperModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace GoWorkFactoryBusinessLogic.BusinessLogics
{
    public class MailLogic
    {
        private static string smtpClientHost;
        private static int smtpClientPort;
        private static string mailLogin;
        private static string mailPassword;

        public static void MailConfig(MailSettings config)
        {
            smtpClientHost = config.SmtpClientHost;
            smtpClientPort = config.SmtpClientPort;
            mailLogin = config.MailLogin;
            mailPassword = config.MailPassword;
        }

        public static async void MailSendAsync(MailSendInfo info)
        {
            if (string.IsNullOrEmpty(smtpClientHost) || smtpClientPort == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(mailLogin) || string.IsNullOrEmpty(mailPassword))
            {
                return;
            }
            if (string.IsNullOrEmpty(info.MailAddress) || string.IsNullOrEmpty(info.Subject) || string.IsNullOrEmpty(info.Text))
            {
                return;
            }
            using (var objMailMessage = new MailMessage())
            {
                using (var objSmtpClient = new SmtpClient(smtpClientHost, smtpClientPort))
                {
                    try
                    {
                        objMailMessage.From = new MailAddress(mailLogin);
                        objMailMessage.To.Add(new MailAddress(info.MailAddress));
                        objMailMessage.Subject = info.Subject;
                        objMailMessage.Body = info.Text;
                        objMailMessage.SubjectEncoding = Encoding.UTF8;
                        objMailMessage.BodyEncoding = Encoding.UTF8;
                        objMailMessage.IsBodyHtml = info.IsBodyHtml;
                        info.Attachments?.ForEach(x =>
                        {
                            ContentType ct = new ContentType(x.ContentType);
                            Attachment attachment = new Attachment(x.FileData, ct);
                            attachment.ContentDisposition.FileName = x.Name + ".";
                            switch (x.ContentType)
                            {
                                case MimeTypes.Excel:
                                    attachment.ContentDisposition.FileName += "xls";
                                    break;
                                case MimeTypes.Pdf:
                                    attachment.ContentDisposition.FileName += "pdf";
                                    break;
                                case MimeTypes.Word:
                                    attachment.ContentDisposition.FileName += "doc";
                                    break;
                            }
                            objMailMessage.Attachments.Add(attachment);

                        });
                        objSmtpClient.UseDefaultCredentials = false;
                        objSmtpClient.EnableSsl = true;
                        objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        objSmtpClient.Credentials = new NetworkCredential(mailLogin, mailPassword);
                        await Task.Run(() => {
                            objSmtpClient.Send(objMailMessage);
                        });
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
