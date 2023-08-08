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
        var smtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            EnableSsl = true
        };

        smtpClient.Credentials = new NetworkCredential("inncarenova@gmail.com", "dalpxgzynpunqzfw"); //Use the new password, generated from google!
        
        MailMessage message = new MailMessage("inncarenova@gmail.com", string.Join(',',toList));

        message.From = new MailAddress("inncarenova@gmail.com", "INNCare");
        message.Subject = "USHAŞ Tedavi Planı Talebi";
        message.IsBodyHtml = true;
        message.Body = body;
        smtpClient.Send(message);
    }
}