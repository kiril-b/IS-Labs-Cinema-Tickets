using System.Net.Mail;
using CinemaTicketsDomain.DomainModels;
using CinemaTicketServices.Interface;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CinemaTicketServices.Implementation;

public class MailService : IMailService {
    private readonly EmailSettings _emailSettings;

    public MailService(EmailSettings emailSettings) {
        _emailSettings = emailSettings;
    }

    public void SendEmail(string to, string subject, string content) {
        var email = new MimeMessage();
        email.To.Add(MailboxAddress.Parse(to));
        email.From.Add(MailboxAddress.Parse(_emailSettings.SmtpUserName));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Plain) { Text = content };

        using var smtp = new SmtpClient();
        var socketOptions = _emailSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
        smtp.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpServerPort, socketOptions);
        if (!string.IsNullOrEmpty(_emailSettings.SmtpUserName)) {
            smtp.Authenticate(_emailSettings.SmtpUserName, _emailSettings.SmtpPassword);
        }
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}