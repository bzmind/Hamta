using MediatR;
using Shop.Domain.Category_Aggregate;

namespace Shop.Application.Category_Use_Cases.Create;

public class CreateCategoryCommand : IRequest
{
    public long? ParentId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public List<Category> SubCategories { get; private set; }
    public List<CategorySpecification> Specifications { get; private set; }

    public CreateCategoryCommand(string title, string slug, List<Category> subCategories,
        List<CategorySpecification> specifications)
    {
        Title = title;
        Slug = slug;
        SubCategories = subCategories;
        Specifications = specifications;
    }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
{
    public Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}