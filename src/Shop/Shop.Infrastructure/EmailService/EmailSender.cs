using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Shop.Infrastructure.EmailService._DTOs;

namespace Shop.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmail(EmailDto emailDto)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config["EmailConfig:Username"]));
        email.To.Add(MailboxAddress.Parse(emailDto.To));
        email.Subject = emailDto.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = emailDto.Body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_config["EmailConfig:Host"], 587, SecureSocketOptions.StartTlsWhenAvailable);
        await smtp.AuthenticateAsync(_config["EmailConfig:Username"], _config["EmailConfig:Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}