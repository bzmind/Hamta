using Common.Application;
using Shop.Application.Entities.Banners.Create;
using Shop.Application.Entities.Banners.Edit;
using Shop.Query.Entities._DTOs;

namespace Shop.Presentation.Facade.Entities.Banner;

public interface IBannerFacade
{
    Task<OperationResult<long>> Create(CreateBannerCommand command);
    Task<OperationResult> Edit(EditBannerCommand command);
    Task<OperationResult> Remove(long id);

    Task<BannerDto?> GetById(long id);
    Task<List<BannerDto>> GetAll();
}