﻿using Common.Domain.Base_Classes;

namespace Shop.Domain.Customer_Aggregate;

public class CustomerFavoriteItem : BaseEntity
{
    public long CustomerId { get; private set; }
    public long ProductId { get; private set; }

    public CustomerFavoriteItem(long customerId, long productId)
    {
        CustomerId = customerId;
        ProductId = productId;
    }
}