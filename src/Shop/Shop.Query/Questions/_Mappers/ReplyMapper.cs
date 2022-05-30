﻿using Shop.Domain.QuestionAggregate;
using Shop.Query.Questions._DTOs;

namespace Shop.Query.Questions._Mappers;

internal static class ReplyMapper
{
    public static ReplyDto MapToReplyDto(this Reply? reply, string customerFullName)
    {
        if (reply == null)
            return null;

        return new ReplyDto
        {
            Id = reply.Id,
            CreationDate = reply.CreationDate,
            QuestionId = reply.QuestionId,
            ProductId = reply.ProductId,
            CustomerId = reply.CustomerId,
            CustomerFullName = customerFullName,
            Description = reply.Description,
            Status = reply.Status
        };
    }
}