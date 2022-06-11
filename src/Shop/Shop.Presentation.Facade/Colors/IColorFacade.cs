using Common.Application;
using Shop.Application.Colors.Create;
using Shop.Application.Colors.Edit;
using Shop.Query.Colors._DTOs;

namespace Shop.Presentation.Facade.Colors;

public interface IColorFacade
{
    Task<OperationResult<long>> Create(CreateColorCommand command);
    Task<OperationResult> Edit(EditColorCommand command);

    Task<ColorDto?> GetById(long id);
    Task<ColorFilterResult> GetByFilter(ColorFilterParams filterParams);
}