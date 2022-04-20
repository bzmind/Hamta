using Shop.Domain.QuestionAggregate;
using Shop.Domain.QuestionAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Questions;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{
    public QuestionRepository(ShopContext context) : base(context)
    {
    }
}