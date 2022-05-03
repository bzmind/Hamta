using Common.Application;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.SetDislikes;
using Shop.Application.Comments.SetLikes;
using Shop.Application.Comments.SetStatus;
using Shop.Query.Comments._DTOs;

namespace Shop.Presentation.Facade.Comments;

public interface ICommentFacade
{
    Task<OperationResult> Create(CreateCommentCommand command);
    Task<OperationResult> SetStatus(SetCommentStatusCommand command);
    Task<OperationResult> SetLikes(SetCommentLikesCommand command);
    Task<OperationResult> SetDislikes(SetCommentDislikesCommand command);

    Task<CommentDto?> GetCommentById(long id);
    Task<CommentFilterResult> GetCommentByFilter(CommentFilterParams filterParams);
}