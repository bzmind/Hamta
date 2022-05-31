using Common.Query.BaseClasses;

namespace Shop.Query.Users._DTOs;

public class UserFavoriteItemDto : BaseDto
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductMainImage { get; set; }
    public int? ProductPrice { get; set; }
    public float? AverageScore { get; set; }
    public bool? IsAvailable { get; set; }
}