﻿using Common.Api;
using Shop.API.ViewModels.Shippings;
using Shop.Query.Shippings._DTOs;

namespace Shop.UI.Services.Shippings;

public interface IShippingService
{
    Task<ApiResult> Create(CreateShippingViewModel model);
    Task<ApiResult> Edit(EditShippingViewModel model);
    Task<ApiResult> Remove(long shippingId);

    Task<ApiResult<ShippingDto?>> GetById(long shippingId);
    Task<ApiResult<List<ShippingDto>>> GetAll();
}