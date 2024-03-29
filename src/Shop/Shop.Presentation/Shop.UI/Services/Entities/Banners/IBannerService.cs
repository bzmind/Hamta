﻿using Common.Api;
using Shop.API.ViewModels.Entities.Banner;
using Shop.Query.Entities._DTOs;

namespace Shop.UI.Services.Entities.Banners;

public interface IBannerService
{
    Task<ApiResult> Create(CreateBannerViewModel model);
    Task<ApiResult> Edit(EditBannerViewModel model);
    Task<ApiResult> Remove(long id);

    Task<ApiResult<BannerDto?>> GetById(long id);
    Task<ApiResult<List<BannerDto>>> GetAll();
}