using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.UserAggregate;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.FavoriteItems.AddFavoriteItem;

public record AddUserFavoriteItemCommand(long UserId, long ProductId) : IBaseCommand;

public class AddUserFavoriteItemCommandHandler : IBaseCommandHandler<AddUserFavoriteItemCommand>
{
    private readonly IUserRepository _userRepository;

    public AddUserFavoriteItemCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(AddUserFavoriteItemCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        if (user.FavoriteItems.Any(fi => fi.ProductId == request.ProductId))
            return OperationResult.Error("این آیتم تکراری است");

        var favoriteItem = new UserFavoriteItem(request.UserId, request.ProductId);
        user.AddFavoriteItem(favoriteItem);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}