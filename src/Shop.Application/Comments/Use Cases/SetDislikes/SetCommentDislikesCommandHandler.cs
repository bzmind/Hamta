﻿using Common.Application;
using Common.Application.Base_Classes;
using Shop.Domain.Comment_Aggregate.Repository;

namespace Shop.Application.Comments.Use_Cases.SetDislikes;

public record SetCommentDislikesCommand(long CommentId, long CustomerId) : IBaseCommand;

public class SetCommentDislikesCommandHandler : IBaseCommandHandler<SetCommentDislikesCommand>
{
    private readonly ICommentRepository _commentRepository;

    public SetCommentDislikesCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<OperationResult> Handle(SetCommentDislikesCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetAsTrackingAsync(request.CommentId);

        if (comment == null)
            return OperationResult.NotFound();

        comment.SetDislikes(request.CustomerId);

        await _commentRepository.SaveAsync();
        return OperationResult.Success();
    }
}