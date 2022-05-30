using Common.Application;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Remove;
using Shop.Application.Comments.SetDislikes;
using Shop.Application.Comments.SetLikes;
using Shop.Application.Comments.SetStatus;
using Shop.Query.Comments._DTOs;

namespace Shop.Presentation.Facade.Comments;

public interface ICommentFacade
{
    Task<OperationResult<long>> Create(CreateCommentCommand command);
    Task<OperationResult> SetStatus(SetCommentStatusCommand command);
    Task<OperationResult> SetLikes(SetCommentLikesCommand command);
    Task<OperationResult> SetDislikes(SetCommentDislikesCommand command);
    Task<OperationResult> Remove(long commentId);

    Task<CommentDto?> GetById(long id);
    Task<CommentFilterResult> GetByFilter(CommentFilterParams filterParams);
}