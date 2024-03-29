﻿using Shop.Domain.SellerAggregate;
using Shop.Query.Sellers._DTOs;

namespace Shop.Query.Sellers._Mappers;

internal static class SellerMapper
{
    public static SellerDto MapToSellerDto(this Seller? seller)
    {
        if (seller == null)
            return null;

        return new SellerDto
        {
            Id = seller.Id,
            CreationDate = seller.CreationDate,
            UserId = seller.UserId,
            ShopName = seller.ShopName,
            NationalCode = seller.NationalCode,
            Status = seller.Status
        };
    }
}