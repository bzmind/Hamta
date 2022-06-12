﻿using Shop.Domain.OrderAggregate;
using Shop.UI.Models._Filters;

namespace Shop.UI.Models.Orders;

public class OrderFilterParamsViewModel : BaseFilterParamsViewModel
{
    public long? UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Order.OrderStatus? Status { get; set; }
}