using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

public static class MailDelivery
{
    public static void SendReport(string from, string password, string to, string file)
    {
        try
        {
            var message = new MailMessage();
            message.From = new MailAddress(from);
            message.To.Add(to);
            message.Subject = "space mission report";
            message.Body = "see attached CSV with shortest path data.";
            message.Attachments.Add(new Attachment(file));

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true
            };

            smtp.Send(message);
            Console.WriteLine("email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("error sending email: " + ex.Message);
        }
    }
}
