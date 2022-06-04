using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.SetSubscriptionToNews;

public record SetUserSubscriptionToNewsCommand(long UserId, bool Subscription) : IBaseCommand;

public class SetUserSubscriptionToNewsCommandHandler
    : IBaseCommandHandler<SetUserSubscriptionToNewsCommand>
{
    private readonly IUserRepository _userRepository;

    public SetUserSubscriptionToNewsCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(SetUserSubscriptionToNewsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound();

        user.SetSubscriptionToNews(request.Subscription);
        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}