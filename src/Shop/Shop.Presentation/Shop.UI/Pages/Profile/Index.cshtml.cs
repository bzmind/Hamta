using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Infrastructure.EmailService;
using Shop.Infrastructure.EmailService._DTOs;
using Shop.UI.SetupClasses.RazorUtility;

namespace Shop.UI.Pages.Profile;

[Authorize]
[BindProperties]
public class IndexModel : BaseRazorPage
{
    private readonly IEmailSender _emailSender;

    public IndexModel(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task<IActionResult> OnGet()
    {
        //await _emailSender.SendEmail(new EmailDto
        //{
        //    To = "smh.gamism@gmail.com",
        //    Subject = "Test Email",
        //    Body = "<h1>Hi, this is a test email</h1>"
        //});
        return Page();
    }
}