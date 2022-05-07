namespace Common.Application;

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
        => new (SuccessMessage, OperationStatusCode.Success);

    public static OperationResult Success(string successMessage)
        => new (successMessage, OperationStatusCode.Success);

    public static OperationResult Error()
        => new (ErrorMessage, OperationStatusCode.Error);

    public static OperationResult Error(string errorMessage)
        => new (errorMessage, OperationStatusCode.Error);

    public static OperationResult NotFound()
        => new (NotFoundMessage, OperationStatusCode.NotFound);

    public static OperationResult NotFound(string notFoundMessage)
        => new (notFoundMessage, OperationStatusCode.NotFound);
}

public enum OperationStatusCode
{
    Success = 200,
    Error = 10,
    NotFound = 404
}