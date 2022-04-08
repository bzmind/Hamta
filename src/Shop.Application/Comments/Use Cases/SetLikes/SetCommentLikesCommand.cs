using Common.Application;
using Common.Application.Base_Classes;
using Shop.Domain.Comment_Aggregate.Repository;

namespace Shop.Application.Comments.Use_Cases.SetLikes;

public record SetCommentLikesCommand(long CommentId, long CustomerId) : IBaseCommand;

public class SetCommentLikesCommandHandler : IBaseCommandHandler<SetCommentLikesCommand>
{
    private readonly ICommentRepository _commentRepository;

    public SetCommentLikesCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<OperationResult> Handle(SetCommentLikesCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetAsTrackingAsync(request.CommentId);

        if (comment == null)
            return OperationResult.NotFound();

        comment.SetLikes(request.CustomerId);

        await _commentRepository.SaveAsync();
        return OperationResult.Success();
    }
}