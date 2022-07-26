using Common.Query.BaseClasses.FilterQuery;
using Shop.Domain.SellerAggregate;

namespace Shop.Query.Sellers._DTOs;

public class SellerFilterResult : BaseFilterResult<SellerDto, SellerFilterParams>
{
    
}

public class SellerFilterParams : BaseFilterParams
{
    public string? ShopName { get; set; }
    public string? NationalCode { get; set; }
    public Seller.SellerStatus? Status { get; set; }
}