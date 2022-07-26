using Common.Query.BaseClasses;
using Shop.Domain.SellerAggregate;

namespace Shop.Query.Sellers._DTOs;

public class SellerDto : BaseDto
{
    public long UserId { get; set; }
    public string ShopName { get; set; }
    public string NationalCode { get; set; }
    public Seller.SellerStatus Status { get; set; }
    public List<SellerInventoryDto> Inventories { get; set; }
}