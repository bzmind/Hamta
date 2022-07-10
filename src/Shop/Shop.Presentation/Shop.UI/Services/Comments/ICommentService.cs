using Common.Api;
using Shop.API.CommandViewModels.Comments;
using Shop.Application.Comments.SetStatus;
using Shop.Query.Comments._DTOs;

namespace Shop.UI.Services.Comments;

public interface ICommentService
{
    Task<ApiResult> Create(CreateCommentCommandViewModel model);
    Task<ApiResult> SetStatus(SetCommentStatusCommand model);
    Task<ApiResult> SetLikes(long commentId);
    Task<ApiResult> SetDislikes(long commentId);
    Task<ApiResult> Remove(long commentId);

    Task<CommentDto> GetById(long commentId);
    Task<CommentFilterResult> GetByFilter(CommentFilterParams filterParams);
}