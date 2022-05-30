﻿using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Shop.Domain.CustomerAggregate;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.AddFavoriteItem;

public record AddCustomerFavoriteItemCommand(long CustomerId, long ProductId) : IBaseCommand;

public class AddCustomerFavoriteItemCommandHandler : IBaseCommandHandler<AddCustomerFavoriteItemCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public AddCustomerFavoriteItemCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult> Handle(AddCustomerFavoriteItemCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsTrackingAsync(request.CustomerId);

        if (customer == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        if (customer.FavoriteItems.Any(fi => fi.ProductId == request.ProductId))
            return OperationResult.Error("این آیتم تکراری است");

        var favoriteItem = new CustomerFavoriteItem(request.CustomerId, request.ProductId);
        customer.AddFavoriteItem(favoriteItem);

        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}