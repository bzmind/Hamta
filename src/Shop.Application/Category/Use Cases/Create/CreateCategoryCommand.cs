using MediatR;
using Shop.Domain.Category_Aggregate;
using Shop.Domain.Category_Aggregate.Repository;
using Shop.Domain.Category_Aggregate.Services;

namespace Shop.Application.Category_Use_Cases.Use_Cases.Create;

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
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository,
        ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Title, request.Slug, _categoryDomainService);

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveAsync();
        return Unit.Value;
    }
}

public class CreateCategoryCommandValidation : 
{

}