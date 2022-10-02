using Shop.Query.Products._DTOs;
using Shop.UI.Services.Entities.Banners;
using Shop.UI.Services.Entities.Sliders;
using Shop.UI.Services.Products;
using Shop.UI.ViewModels.MainPage;

namespace Shop.UI.Services.MainPage;

public class MainPageService : IMainPageService
{
    private readonly ISliderService _sliderService;
    private readonly IBannerService _bannerService;
    private readonly IProductService _productService;

    public MainPageService(ISliderService sliderService, IBannerService bannerService,
        IProductService productService)
    {
        _sliderService = sliderService;
        _bannerService = bannerService;
        _productService = productService;
    }

    public async Task<MainPageViewModel> GetMainPageData()
    {
        var sliders = await _sliderService.GetAll();
        var banners = await _bannerService.GetAll();
        var saleProducts = await _productService.GetForShopByFilter(new ProductForShopFilterParams
        {
            PageId = 1,
            Take = 5,
            MinDiscountPercentage = 15
        });
        var mostSoldProducts = await _productService.GetForShopByFilter(new ProductForShopFilterParams
        {
            PageId = 1,
            Take = 9
        });
        var categoryProducts = await _productService.GetForShopByFilter(new ProductForShopFilterParams
        {
            PageId = 1,
            Take = 8,
            CategoryId = 1
        });

        return new MainPageViewModel
        {
            Sliders = sliders,
            Banners = banners,
            SaleProducts = saleProducts.Data,
            MostSoldProducts = mostSoldProducts.Data,
            CategoryProducts = categoryProducts.Data
        };
    }
}