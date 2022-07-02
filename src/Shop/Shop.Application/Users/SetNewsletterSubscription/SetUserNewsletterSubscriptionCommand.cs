using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.SetNewsletterSubscription;

public record SetUserNewsletterSubscriptionCommand(long UserId) : IBaseCommand<bool>;

public class SetUserNewsletterSubscriptionCommandHandler : IBaseCommandHandler<SetUserNewsletterSubscriptionCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public SetUserNewsletterSubscriptionCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult<bool>> Handle(SetUserNewsletterSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult<bool>.NotFound();

        if (string.IsNullOrWhiteSpace(user.Email))
            return OperationResult<bool>.NotFound("برای ثبت‌نام در خبرنامه، " +
                                                  "ابتدا ایمیل خود را در قسمت ویرایش حساب وارد کنید.");

        user.SetNewsletterSubscription();

        await _userRepository.SaveAsync();
        return OperationResult<bool>.Success(user.IsSubscribedToNewsletter);
    }
}