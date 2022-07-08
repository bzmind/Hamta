using Common.Application.Utility.FileUtility;

namespace Shop.UI.SetupClasses.ServerSide;

public static class ServerSettings
{
    public static string ServerPath =>
        "https://localhost:7087";

    public static string ServerAvatarsPath =>
        $"{ServerPath}/{Directories.UserAvatars}".Replace("wwwroot/", "");

    public static string ServerBannerImagesPath =>
        $"{ServerPath}/{Directories.BannerImages}".Replace("wwwroot/", "");

    public static string ServerProductGalleryImagesPath =>
        $"{ServerPath}/{Directories.ProductGalleryImages}".Replace("wwwroot/", "");

    public static string ServerProductMainImagesPath =>
        $"{ServerPath}/{Directories.ProductMainImages}".Replace("wwwroot/", "");

    public static string ServerSliderImagesPath =>
        $"{ServerPath}/{Directories.SliderImages}".Replace("wwwroot/", "");
}