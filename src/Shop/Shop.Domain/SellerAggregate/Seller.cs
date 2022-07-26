using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.Utility;
using Shop.Domain.SellerAggregate.Services;

namespace Shop.Domain.SellerAggregate;

public class Seller : BaseAggregateRoot
{
    public long UserId { get; set; }
    public string ShopName { get; set; }
    public string NationalCode { get; set; }
    public SellerStatus Status { get; set; }

    private readonly List<SellerInventory> _inventories = new();
    public IEnumerable<SellerInventory> Inventories => _inventories.ToList();

    public enum SellerStatus
    {
        Pending,
        Rejected,
        Accepted,
        Deactivated
    }

    private Seller()
    {
    }

    public Seller(long userId, string shopName, string nationalCode, ISellerDomainService domainService)
    {
        Guard(shopName, nationalCode);
        UserId = userId;
        ShopName = shopName;
        NationalCode = nationalCode;
        Status = SellerStatus.Pending;

        if (domainService.IsSellerValid(this) == false)
            throw new InvalidDataDomainException("Seller is not valid");
    }

    public void Edit(string shopName, string nationalCode, ISellerDomainService domainService)
    {
        Guard(shopName, nationalCode);
        if (nationalCode != NationalCode)
            if (domainService.IsDuplicateNationalCode(nationalCode))
                throw new InvalidDataDomainException("This national code belongs to someone else");

        ShopName = shopName;
        NationalCode = nationalCode;
    }

    public void AddInventory(SellerInventory inventory)
    {
        if (Inventories.Any(sellerInventory => sellerInventory.ProductId == inventory.ProductId))
            throw new InvalidDataDomainException("Inventory with the same product already exists");

        _inventories.Add(inventory);
    }

    public void EditInventory(long inventoryId, long productId, int quantity, int price, long colorId,
        int discountPercentage)
    {
        var inventory = Inventories.FirstOrDefault(inventory => inventory.Id == inventoryId);
        if (inventory == null)
            throw new DataNotFoundDomainException("Inventory not found");

        inventory.Edit(productId, quantity, price, colorId, discountPercentage);
    }

    public void RemoveInventory(long inventoryId)
    {
        var inventory = Inventories.FirstOrDefault(inventory => inventory.Id == inventoryId);
        if (inventory == null)
            throw new DataNotFoundDomainException("Inventory not found");

        _inventories.Remove(inventory);
    }

    public void IncreaseInventoryQuantity(long inventoryId, int quantity)
    {
        var inventory = Inventories.FirstOrDefault(inventory => inventory.Id == inventoryId);
        if (inventory == null)
            throw new DataNotFoundDomainException("Inventory not found");

        inventory.IncreaseQuantity(quantity);
    }

    public void DecreaseInventoryQuantity(long inventoryId, int quantity)
    {
        var inventory = Inventories.FirstOrDefault(inventory => inventory.Id == inventoryId);
        if (inventory == null)
            throw new DataNotFoundDomainException("Inventory not found");

        inventory.DecreaseQuantity(quantity);
    }

    public void SetStatus(SellerStatus status)
    {
        Status = status;
    }

    private void Guard(string shopName, string nationalCode)
    {
        NullOrEmptyDataDomainException.CheckString(shopName, nameof(shopName));
        NullOrEmptyDataDomainException.CheckString(nationalCode, nameof(nationalCode));
        if (IranianNationalCodeChecker.IsValid(nationalCode) == false)
            throw new InvalidDataDomainException("Invalid national code");
    }
}