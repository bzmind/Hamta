using Shop.Domain.CommentAggregate;
using Shop.Domain.CommentAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Comments;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(ShopContext shopContext) : base(shopContext)
    {
    }
}