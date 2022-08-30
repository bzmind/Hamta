using Common.Query.BaseClasses;

namespace Shop.Query.Products._DTOs;

public class ProductGalleryImageDto : BaseDto
{
    public long ProductId { get; set; }
    public string Name { get; set; }
    public int Sequence { get; set; }
}