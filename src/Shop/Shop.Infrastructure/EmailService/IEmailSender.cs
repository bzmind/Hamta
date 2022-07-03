using Shop.Infrastructure.EmailService._DTOs;

namespace Shop.Infrastructure.EmailService;

public interface IEmailSender
{
    Task SendEmail(EmailDto emailDto);
}