using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Questions._DTOs;
using Shop.Query.Questions._Mappers;

namespace Shop.Query.Questions.GetById;

public record GetQuestionByIdQuery(long QuestionId) : IBaseQuery<QuestionDto?>;

public class GetQuestionByIdQueryHandler : IBaseQueryHandler<GetQuestionByIdQuery, QuestionDto?>
{
    private readonly ShopContext _shopContext;

    public GetQuestionByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<QuestionDto?> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        var tables = await _shopContext.Questions
            .Where(q => q.Id == request.QuestionId)
            .Join(
                _shopContext.Users,
                q => q.UserId,
                c => c.Id,
                (question, user) => new
                {
                    question,
                    user
                })
            .FirstOrDefaultAsync(cancellationToken);

        var repliesUserIds = new List<long>();
        tables?.question.Replies.ToList().ForEach(rDto =>
        {
            repliesUserIds.Add(rDto.UserId);
        });

        var users = await _shopContext.Users
            .Where(c => repliesUserIds.Contains(c.Id)).ToListAsync(cancellationToken);

        var questionDto = tables.question.MapToQuestionDto(tables.user.FullName);

        questionDto.Replies.ForEach(rDto =>
        {
            var user = users.First(c => c.Id == rDto.UserId);
            rDto.UserFullName = user.FullName;
        });

        return questionDto;
    }
}