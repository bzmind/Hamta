﻿namespace Common.Application;

public class OperationResult
{
    public string Message { get; set; }
    public OperationStatusCode StatusCode { get; set; }

    private const string SuccessMessage = "عملیات با موفقیت انجام شد";
    private const string ErrorMessage = "خطایی در عملیات رخ داده است";
    private const string NotFoundMessage = "اطلاعات یافت نشد";

    public OperationResult(string message, OperationStatusCode statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }

    public static OperationResult Success()
        => new(SuccessMessage, OperationStatusCode.Success);

    public static OperationResult Success(string successMessage)
        => new(successMessage, OperationStatusCode.Success);

    public static OperationResult Error()
        => new(ErrorMessage, OperationStatusCode.ServerError);

    public static OperationResult Error(string errorMessage)
        => new(errorMessage, OperationStatusCode.ServerError);

    public static OperationResult NotFound()
        => new(NotFoundMessage, OperationStatusCode.NotFound);

    public static OperationResult NotFound(string notFoundMessage)
        => new(notFoundMessage, OperationStatusCode.NotFound);
}

public class OperationResult<TData>
{
    public TData? Data { get; set; }
    public string Message { get; set; }
    public OperationStatusCode StatusCode { get; set; }

    private const string SuccessMessage = "عملیات با موفقیت انجام شد";
    private const string ErrorMessage = "خطایی در عملیات رخ داده است";
    private const string NotFoundMessage = "اطلاعات یافت نشد";

    public OperationResult(TData? data, string message, OperationStatusCode statusCode)
    {
        Data = data;
        Message = message;
        StatusCode = statusCode;
    }

    public static OperationResult<TData> Success(TData data, string? message = SuccessMessage)
        => new(data, message ?? SuccessMessage, OperationStatusCode.Success);

    public static OperationResult<TData> Error()
        => new(default, ErrorMessage, OperationStatusCode.ServerError);

    public static OperationResult<TData> Error(string? message = ErrorMessage)
        => new(default, message ?? ErrorMessage, OperationStatusCode.ServerError);

    public static OperationResult<TData> Error(TData? data = default, string? message = ErrorMessage)
        => new(data, message ?? ErrorMessage, OperationStatusCode.ServerError);

    public static OperationResult<TData> NotFound()
        => new(default, NotFoundMessage, OperationStatusCode.NotFound);

    public static OperationResult<TData> NotFound(string? message = NotFoundMessage)
        => new(default, message ?? NotFoundMessage, OperationStatusCode.NotFound);

    public static OperationResult<TData> NotFound(TData? data = default, string? message = NotFoundMessage)
        => new(data, message ?? NotFoundMessage, OperationStatusCode.NotFound);
}

public enum OperationStatusCode
{
    Success = 200,
    BadRequest = 400,
    NotFound = 404,
    TooManyRequests = 429,
    ServerError = 500
}