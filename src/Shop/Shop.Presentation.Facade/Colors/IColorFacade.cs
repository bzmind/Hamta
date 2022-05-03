using Common.Application;
using Shop.Application.Colors.Create;
using Shop.Application.Colors.Edit;
using Shop.Application.Colors.Remove;
using Shop.Query.Colors._DTOs;

namespace Shop.Presentation.Facade.Colors;

public interface IColorFacade
{
    Task<OperationResult> Create(CreateColorCommand command);
    Task<OperationResult> Edit(EditColorCommand command);
    Task<OperationResult> Remove(RemoveColorCommand command);

    Task<ColorDto?> GetColorById(long id);
    Task<ColorFilterResult> GetColorByFilter(ColorFilterParam filterParams);
}