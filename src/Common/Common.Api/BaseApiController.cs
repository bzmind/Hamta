﻿using Common.Application;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Common.Api;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected ApiResult CommandResult(OperationResult result)
    {
        return new ApiResult
        {
            IsSuccessful = result.StatusCode == OperationStatusCode.Success,
            MetaData = new MetaData
            {
                Message = result.Message,
                ApiStatusCode = result.StatusCode.MapToApiStatusCode()
            }
        };
    }

    protected ApiResult<TData?> CommandResult<TData>(OperationResult<TData> result,
        HttpStatusCode statusCode = HttpStatusCode.OK, string? resultUrl = null)
    {
        var isSuccessful = result.StatusCode == OperationStatusCode.Success;

        if (isSuccessful)
        {
            HttpContext.Response.StatusCode = (int)statusCode;

            if (!string.IsNullOrWhiteSpace(resultUrl))
                HttpContext.Response.Headers.Add("ResultUrl", resultUrl);
        }

        return new ApiResult<TData?>
        {
            IsSuccessful = result.StatusCode == OperationStatusCode.Success,
            Data = isSuccessful ? result.Data : default(TData),
            MetaData = new MetaData
            {
                Message = result.Message,
                ApiStatusCode = result.StatusCode.MapToApiStatusCode()
            }
        };
    }

    protected ApiResult<TData> QueryResult<TData>(TData data)
    {
        return new ApiResult<TData>
        {
            IsSuccessful = true,
            Data = data,
            MetaData = new MetaData
            {
                Message = "عملیات با موفقیت انجام شد",
                ApiStatusCode = ApiStatusCode.Success
            }
        };
    }
}