﻿namespace Shop.UI.Models.Shippings;

public class EditShippingCommandViewModel
{
    public long ShippingId { get; set; }
    public string ShippingMethod { get; set; }
    public string ShippingCost { get; set; }
}