using Common.Api;
using Shop.API.ViewModels.Comments;
using Shop.Query.Comments._DTOs;

namespace Shop.UI.Services.Comments;

public interface ICommentService
{
    Task<ApiResult> Create(CreateCommentViewModel model);
    Task<ApiResult> SetStatus(SetCommentStatusViewModel model);
    Task<ApiResult> SetLikes(long commentId);
    Task<ApiResult> SetDislikes(long commentId);
    Task<ApiResult> Remove(long commentId);

    Task<ApiResult<CommentDto?>> GetById(long commentId);
    Task<ApiResult<CommentFilterResult>> GetByFilter(CommentFilterParams filterParams);
    Task<ApiResult<CommentFilterResult>> GetForProduct(ProductCommentFilterParams filterParams);
}