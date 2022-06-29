using Common.Api;
using Shop.API.CommandViewModels.Comments;
using Shop.Application.Comments.SetStatus;
using Shop.Query.Comments._DTOs;

namespace Shop.UI.Services.Comments;

public interface ICommentService
{
    Task<ApiResult?> Create(CreateCommentCommandViewModel model);
    Task<ApiResult?> SetStatus(SetCommentStatusCommand model);
    Task<ApiResult?> SetLikes(long commentId); //This and the bottom one may not work, since the SetLikes/Dislikes controllers at the API end, also need a UserId, which you set to get it from the current user, but if I request from here, can it still get the logged in user on the API? idk
    Task<ApiResult?> SetDislikes(long commentId);
    Task<ApiResult?> Remove(long commentId);

    Task<CommentDto?> GetById(long commentId);
    Task<CommentFilterResult?> GetByFilter(CommentFilterParams filterParams);
}