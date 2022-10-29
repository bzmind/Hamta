using Common.Query.BaseClasses;

namespace Shop.Query.Shippings._DTOs;

public class ShippingDto : BaseDto
{
    public string Name { get; set; }
    public int Cost { get; set; }
}