using Common.Api;
using Common.Application.Utility.Validation;
using CookieManager;
using Shop.Domain.OrderAggregate;
using Shop.Query.Orders._DTOs;
using Shop.UI.Services.Products;
using Shop.UI.Services.Sellers;

namespace Shop.UI.Setup.CookieUtility;

public class CartCookieManager
{
    private readonly ICookieManager _cookieManager;
    private readonly ISellerService _sellerService;
    private readonly IProductService _productService;
    private const string CartCookieName = "CartCookie";

    public CartCookieManager(ICookieManager cookieManager, ISellerService sellerService,
        IProductService productService)
    {
        _cookieManager = cookieManager;
        _sellerService = sellerService;
        _productService = productService;
    }

    public OrderDto? GetCart()
    {
        return _cookieManager.Get<OrderDto>(CartCookieName);
    }

    public async Task<ApiResult> AddItem(long inventoryId, int count)
    {
        var cart = GetCart();

        var inventory = await _sellerService.GetInventoryById(inventoryId);
        if (inventory == null)
            return ApiResult.Error(ValidationMessages.FieldNotFound("انبار"));

        var product = await _productService.GetById(inventory.ProductId);

        if (cart == null)
        {
            var order = new OrderDto
            {
                Id = 1,
                UserId = 1,
                CreationDate = DateTime.Now,
                Status = Order.OrderStatus.Pending,
                Items = new List<OrderItemDto>
                {
                    new()
                    {
                        Id = GenerateId(),
                        OrderId = 1,
                        CreationDate = DateTime.Now,
                        InventoryId = inventoryId,
                        InventoryDiscountPercentage = inventory.DiscountPercentage,
                        InventoryShopName = inventory.ShopName,
                        InventoryQuantity = inventory.Quantity,
                        ColorName = inventory.ColorName,
                        ColorCode = inventory.ColorCode,
                        ProductName = inventory.ProductName,
                        ProductMainImage = inventory.ProductMainImage,
                        ProductSlug = product.Slug,
                        Count = count,
                        Price = inventory.Price
                    }
                }
            };

            SetCartCookie(order);
            return ApiResult.Success();
        }

        if (cart.Items.Any(i => i.InventoryId == inventoryId))
        {
            var item = cart.Items.First(i => i.InventoryId == inventoryId);
            if (inventory.Quantity >= item.Count + count)
                item.Count += count;
            else
                return ApiResult.Error("تعداد محصولات سفارش داده شده بیشتر از موجودی است.");
        }
        else
        {
            var newItem = new OrderItemDto
            {
                Id = GenerateId(),
                OrderId = 1,
                CreationDate = DateTime.Now,
                InventoryId = inventoryId,
                InventoryDiscountPercentage = inventory.DiscountPercentage,
                InventoryShopName = inventory.ShopName,
                InventoryQuantity = inventory.Quantity,
                ColorName = inventory.ColorName,
                ColorCode = inventory.ColorCode,
                ProductName = inventory.ProductName,
                ProductMainImage = inventory.ProductMainImage,
                ProductSlug = product.Slug,
                Count = count,
                Price = inventory.Price
            };
            cart.Items.Add(newItem);
        }

        SetCartCookie(cart);
        return ApiResult.Success();
    }

    public ApiResult RemoveItem(long itemId)
    {
        var cart = GetCart();
        if (cart == null)
            return ApiResult.Error("سبد خرید یافت نشد.");

        var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            return ApiResult.Error("محصول در سبد خرید یافت نشد.");

        cart.Items.Remove(item);

        SetCartCookie(cart);
        return ApiResult.Success();
    }

    public async Task<ApiResult> Increase(long inventoryId, long itemId)
    {
        var cart = GetCart();
        if (cart == null)
            return ApiResult.Error("سبد خرید یافت نشد.");

        var item = cart.Items.FirstOrDefault(item => item.Id == itemId);
        if (item == null)
            return ApiResult.Error("محصول در سبد خرید یافت نشد.");

        var inventory = await _sellerService.GetInventoryById(inventoryId);
        if (inventory == null)
            return ApiResult.Error(ValidationMessages.FieldNotFound("انبار"));

        if (inventory.Quantity - item.Count <= 0)
            return ApiResult.Error("تعداد محصولات سفارش داده شده بیشتر از موجودی است.");

        item.Count += 1;
        SetCartCookie(cart);
        return ApiResult.Success();
    }

    public ApiResult Decrease(long itemId)
    {
        var cart = GetCart();
        if (cart == null)
            return ApiResult.Error("سبد خرید یافت نشد.");

        var item = cart.Items.FirstOrDefault(item => item.Id == itemId);
        if (item == null)
            return ApiResult.Error("محصول در سبد خرید یافت نشد.");

        if (item.Count <= 1)
            cart.Items.Remove(item);
        else
            item.Count -= 1;
        
        SetCartCookie(cart);
        return ApiResult.Success();
    }

    public void RemoveCart()
    {
        _cookieManager.Remove(CartCookieName);
    }

    private void SetCartCookie(OrderDto order)
    {
        _cookieManager.Set(CartCookieName, order, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTimeOffset.Now.AddDays(7)
        });
    }

    private long GenerateId()
    {
        var random = new Random();
        var number = random.Next(0, 10000) * 6 ^ 2 + random.Next(6, 1000000);
        return number;
    }
}