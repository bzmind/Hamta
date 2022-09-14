using Shop.Query.Entities._DTOs;
using Shop.Query.Products._DTOs;

namespace Shop.UI.ViewModels.MainPage;

public class MainPageViewModel
{
    public List<SliderDto> Sliders { get; set; }
    public List<BannerDto> Banners { get; set; }
    public List<ProductFilterDto> SaleProducts { get; set; }
    public List<ProductFilterDto> MostSoldProducts { get; set; }
    public List<ProductFilterDto> CategoryProducts { get; set; }
}