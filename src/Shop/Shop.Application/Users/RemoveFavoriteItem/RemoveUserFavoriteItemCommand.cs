using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.RemoveFavoriteItem;

public record RemoveUserFavoriteItemCommand(long UserId, long FavoriteItemId) : IBaseCommand;

public class RemoveUserFavoriteItemCommandHandler : IBaseCommandHandler<RemoveUserFavoriteItemCommand>
{
    private readonly IUserRepository _userRepository;

    public RemoveUserFavoriteItemCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(RemoveUserFavoriteItemCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound();

        user.RemoveFavoriteItem(request.FavoriteItemId);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}