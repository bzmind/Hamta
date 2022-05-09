using Common.Application;

namespace Common.Api;

public static class ApiStatusCodeMapper
{
    public static ApiStatusCode MapToApiStatusCode(this OperationStatusCode statusCode)
    {
        switch (statusCode)
        {
            case OperationStatusCode.Success:
                return ApiStatusCode.Success;

            case OperationStatusCode.Error:
                return ApiStatusCode.ServerError;

            case OperationStatusCode.NotFound:
                return ApiStatusCode.NotFound;
        }

        return ApiStatusCode.ServerError;
    }
}