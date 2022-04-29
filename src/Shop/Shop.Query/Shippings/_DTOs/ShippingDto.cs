using Common.Query.BaseClasses;

namespace Shop.Query.Shippings._DTOs;

public class ShippingDto : BaseDto
{
    public string ShippingMethod { get; set; }
    public int ShippingCost { get; set; }
}