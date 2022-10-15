using Microsoft.AspNetCore.Mvc;
using Shop.Infrastructure.EmailService;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Profile;

[BindProperties]
public class IndexModel : BaseRazorPage
{
    private readonly IEmailSender _emailSender;

    public IndexModel(IEmailSender emailSender,
        IRazorToStringRenderer razorToStringRenderer) : base(razorToStringRenderer)
    {
        _emailSender = emailSender;
    }

    public void OnGet()
    {
        //await _emailSender.SendEmail(new EmailDto
        //{
        //    To = "smh.gamism@gmail.com",
        //    Subject = "Test Email",
        //    Body = "<h1>Hi, this is a test email</h1>"
        //});
    }
}