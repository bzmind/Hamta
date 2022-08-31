namespace Common.Application.Utility.FileUtility;

public static class ServerPaths
{
    private static string ServerPath =>
        "https://localhost:7087";

    public static string GetAvatarPath(string name) =>
        $"{ServerPath}/{Directories.UserAvatars}/{name}".Replace("wwwroot/", "");

    public static string GetBannerImagePath(string name) =>
        $"{ServerPath}/{Directories.BannerImages}/{name}".Replace("wwwroot/", "");

    public static string GetProductGalleryImagePath(string name) =>
        $"{ServerPath}/{Directories.ProductGalleryImages}/{name}".Replace("wwwroot/", "");

    public static string GetProductReviewImagePath(string name) =>
        $"{ServerPath}/{Directories.ProductReviewImages}/{name}".Replace("wwwroot/", "");

    public static string GetProductMainImagePath(string name) =>
        $"{ServerPath}/{Directories.ProductMainImages}/{name}".Replace("wwwroot/", "");

    public static string GetSliderImagePath(string name) =>
        $"{ServerPath}/{Directories.SliderImages}/{name}".Replace("wwwroot/", "");
}