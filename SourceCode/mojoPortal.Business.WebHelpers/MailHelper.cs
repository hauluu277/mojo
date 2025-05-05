using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Business.WebHelpers
{
    public class MailHelper
    {
        public static bool SendMail(SiteSettings siteSettings, string passMail, string to = "", string subject = "", string body = "")
        {

            if (!string.IsNullOrEmpty(siteSettings.SMTPPassword))
            {
                /// Mail details
                if (string.IsNullOrEmpty(subject))
                {
                    subject = "Trường Đại học Sư phạm - Đại học Thái Nguyên thông báo";
                }
                try
                {
                    MailMessage msg = new MailMessage(siteSettings.DefaultEmailFromAddress, to);
                    msg.Subject = subject;
                    msg.Body = body;
                    msg.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.EnableSsl = siteSettings.SMTPUseSsl;
                    smtp.Host = siteSettings.SMTPServer;
                    msg.SubjectEncoding = System.Text.Encoding.UTF8;
                    msg.BodyEncoding = System.Text.Encoding.UTF8;
                    msg.Priority = MailPriority.Normal;
                    NetworkCredential networkCredential = new NetworkCredential(siteSettings.DefaultEmailFromAddress, passMail);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = networkCredential;
                    smtp.Port = siteSettings.SMTPPort;
                    smtp.Send(msg);
                    return true;
                    /// Enable one of the following method.
                }
                catch (Exception ex)
                {
                    return false;
                    /// Throw exception to higher tier
                    //throw exp;
                }
            }
            return false;
        }

    }
}
