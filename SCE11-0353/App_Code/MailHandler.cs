using System;
using System.IO;
using System.Net.Mail;
using System.Web;

/// <summary>
/// This class handles SMTP operations on behalf of the application.
/// </summary>
public class MailHandler
{
    private const string FromAddress = "admin@ris.com";
    private const string FromName = "RIS Administrator";

    public static void AccountCreated(string to, string firstName, string lastName, string username, string password,
                                      string email,
                                      string mailContentUri)
    {
        var fullName = firstName + " " + lastName;

        // Create message body
        var mailBody = File.ReadAllText(mailContentUri);
        mailBody = mailBody.Replace("##Fullname##", HttpUtility.HtmlDecode(fullName));
        mailBody = mailBody.Replace("##Username##", HttpUtility.HtmlDecode(username));
        mailBody = mailBody.Replace("##Password##", HttpUtility.HtmlDecode(password));

        // Create mail
        var mail = new MailMessage
        {
            Subject = "Account Created",
            From = new MailAddress(FromAddress, FromName),
            Body = HttpUtility.HtmlDecode(mailBody)
        };
        mail.To.Add(new MailAddress(email, fullName));

        // Send mail
        try
        {
            using (var mailServer = new SmtpClient())
            {
                mailServer.Send(mail);
            }
        }
        catch (ArgumentNullException) { }
        catch (ObjectDisposedException) { }
        catch (InvalidOperationException) { }
        catch (SmtpFailedRecipientsException) { }
        catch (SmtpException) { }
    }

    public static void NewPassword(string username, string to, string newPassword, string mailContentUri)
    {
        // Create message body
        var mailBody = File.ReadAllText(mailContentUri);
        mailBody = mailBody.Replace("##Username##", username);
        mailBody = mailBody.Replace("##Password##", newPassword);

        // Create mail
        var mail = new MailMessage
        {
            Subject = "Password Has Been Reset",
            From = new MailAddress(FromAddress, FromName),
            Body = mailBody
        };
        mail.To.Add(new MailAddress(to, username));

        try
        {
            using (var mailServer = new SmtpClient())
            {
                mailServer.Send(mail);
            }
        }
        catch (ArgumentNullException) { }
        catch (ObjectDisposedException) { }
        catch (InvalidOperationException) { }
        catch (SmtpFailedRecipientsException) { }
        catch (SmtpException) { }
    }
}