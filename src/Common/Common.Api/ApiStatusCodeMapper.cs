using Common.Application;

namespace Common.Api;

public static class ApiStatusCodeMapper
{
    public static ApiStatusCode MapToApiStatusCode(this OperationStatusCode statusCode)
    {
        return statusCode switch
        {
            OperationStatusCode.Success => ApiStatusCode.Success,
            OperationStatusCode.BadRequest => ApiStatusCode.BadRequest,
            OperationStatusCode.NotFound => ApiStatusCode.NotFound,
            OperationStatusCode.TooManyRequests => ApiStatusCode.TooManyRequests,
            OperationStatusCode.ServerError => ApiStatusCode.ServerError,
            _ => ApiStatusCode.ServerError
        };
    }

    public static OperationStatusCode MapToOperationStatusCode(this ApiStatusCode statusCode)
    {
        return statusCode switch
        {
            ApiStatusCode.Success => OperationStatusCode.Success,
            ApiStatusCode.BadRequest => OperationStatusCode.BadRequest,
            ApiStatusCode.NotFound => OperationStatusCode.NotFound,
            ApiStatusCode.TooManyRequests => OperationStatusCode.TooManyRequests,
            ApiStatusCode.ServerError => OperationStatusCode.ServerError,
            _ => OperationStatusCode.ServerError
        };
    }
}