using System;
using System.Net.Mail;

/// <summary>
/// This class handles SMTP operations on behalf of the application.
/// </summary>
public class MailHandler
{
    private const string From = "admin@yourdomain.com";
    private const string Subject = "Your password has been reset";

    public static void SendNewPassword(string to, string newPassword)
    {
        var body = "The new password for your account is " + newPassword;
        var mail = new MailMessage(new MailAddress(From), new MailAddress(to))
                       {
                           Body = body,
                           Subject = Subject
                       };
        try
        {
            using (var mailServer = new SmtpClient())
            {
                mailServer.Send(mail);
            }
        }
        catch (ArgumentNullException) {}
        catch (ObjectDisposedException) {}
        catch (InvalidOperationException) {}
        catch (SmtpFailedRecipientsException) {}
        catch (SmtpException) {}
    }
}