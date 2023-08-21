using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace HTS.Common;

public static class Helper
{
    public static void SendMail(List<string> toList, string body)
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
        MailMessage message = new MailMessage(fromEmail, string.Join(',',toList));

        message.From = new MailAddress(fromEmail, "Info USHAŞ");
        message.Subject = "USHAŞ Tedavi Planı Talebi";
        message.IsBodyHtml = true;
        message.Body = body;
        smtpClient.Send(message);
    }
}