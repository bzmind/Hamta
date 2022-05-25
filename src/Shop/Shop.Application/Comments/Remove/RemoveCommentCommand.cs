using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.CommentAggregate.Repository;

namespace Shop.Application.Comments.Remove;

public record RemoveCommentCommand(long CommentId) : IBaseCommand;

public class RemoveCommentCommandHandler : IBaseCommandHandler<RemoveCommentCommand>
{
    private readonly ICommentRepository _commentRepository;

    public RemoveCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<OperationResult> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetAsync(request.CommentId);

        if (comment == null)
            return OperationResult.NotFound();

        _commentRepository.Delete(comment);

        await _commentRepository.SaveAsync();
        return OperationResult.Success();
    }
}