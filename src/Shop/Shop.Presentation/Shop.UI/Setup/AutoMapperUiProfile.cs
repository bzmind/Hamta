using AutoMapper;
using Shop.Query.Categories._DTOs;
using Shop.UI.ViewModels.Categories;

namespace Shop.UI.Setup;

public class AutoMapperUiProfile : Profile
{
    public AutoMapperUiProfile()
    {
        CreateMap<CategoryViewModel, CategoryDto>().ReverseMap();
    }
}