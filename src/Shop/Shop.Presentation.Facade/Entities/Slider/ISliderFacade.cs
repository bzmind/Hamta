using Common.Application;
using Shop.Application.Entities.Sliders.Create;
using Shop.Application.Entities.Sliders.Edit;
using Shop.Query.Entities._DTOs;

namespace Shop.Presentation.Facade.Entities.Slider;

public interface ISliderFacade
{
    Task<OperationResult<long>> Create(CreateSliderCommand command);
    Task<OperationResult> Edit(EditSliderCommand command);
    Task<OperationResult> Remove(long id);

    Task<SliderDto?> GetById(long id);
    Task<List<SliderDto>> GetAll();
}