using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace HTS.Common;

public static class Helper
{
    public static void SendMail(List<string> toList, string body, byte[] file=null, string subject = null,string fileName = null)
    {
        string fromEmail = "info@ushas.com.tr";
        string password = "VWhb8Jo4UZ";
        string smtpServer = "mail.ushas.com.tr";
        var smtpClient = new SmtpClient(smtpServer, 587)
        {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            EnableSsl = true
        };

        smtpClient.Credentials = new NetworkCredential(fromEmail, password);
        MailMessage message = new MailMessage(fromEmail, string.Join(',', toList));

        message.From = new MailAddress(fromEmail, "Info USHAŞ");
        message.Subject = subject ?? "USHAŞ Tedavi Planı Talebi";
        message.IsBodyHtml = true;
        message.Body = body;
#if !DEBUG
        smtpClient.Send(message);
#endif
    }

    public static void SendMail(string to, string body, byte[] file, string subject = null, string fileName = null)
    {
        string fromEmail = "info@ushas.com.tr";
        string password = "VWhb8Jo4UZ";
        string smtpServer = "mail.ushas.com.tr";

        var smtpClient = new SmtpClient(smtpServer, 587)
        {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            EnableSsl = true
        };

        smtpClient.Credentials = new NetworkCredential(fromEmail, password);
        MailMessage message = new MailMessage(fromEmail, to);

        message.From = new MailAddress(fromEmail, "Info USHAŞ");
        message.Subject = subject ?? "USHAŞ Proforma";
        message.IsBodyHtml = true;
        message.Body = body;
        if (file != null)
        {
            Attachment attachment = new Attachment(new MemoryStream(file), new ContentType(MediaTypeNames.Application.Pdf))
            {
                Name = fileName ?? "Proforma.pdf"
            };
            message.Attachments.Add(attachment);
        }
#if !DEBUG
        smtpClient.Send(message);
#endif
    }

}