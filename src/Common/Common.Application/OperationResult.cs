﻿namespace Common.Application;

public class OperationResult
{
    public string Message { get; set; }

    private const string SuccessMessage = "عملیات با موفقیت انجام شد";
    private const string ErrorMessage = "خطایی در عملیات رخ داده است";
    private const string NotFoundMessage = "اطلاعات یافت نشد";

    public OperationResult(string message)
    {
        Message = message;
    }

    public static OperationResult Success()
    {
        return new OperationResult(SuccessMessage);
    }

    public static OperationResult Error()
    {
        return new OperationResult(ErrorMessage);
    }

    public static OperationResult NotFound()
    {
        return new OperationResult(NotFoundMessage);
    }
}