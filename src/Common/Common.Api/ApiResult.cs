namespace Common.Api;

public class ApiResult
{
    protected const string SuccessMessage = "عملیات با موفقیت انجام شد";
    protected const string ErrorMessage = "خطایی در عملیات رخ داده است";
    protected const string NotFoundMessage = "اطلاعات یافت نشد";

    public bool IsSuccessful { get; set; }
    public MetaData MetaData { get; set; }
}

public class ApiResult<TData> : ApiResult
{
    public TData Data { get; set; }

    public static ApiResult<TData> Success(TData data)
    {
        return new ApiResult<TData>
        {
            IsSuccessful = true,
            Data = data,
            MetaData = new MetaData
            {
                ApiStatusCode = ApiStatusCode.Success,
                Message = SuccessMessage
            }
        };
    }
}

public class MetaData
{
    public string? Message { get; set; }
    public ApiStatusCode ApiStatusCode { get; set; }
}

public enum ApiStatusCode
{
    Success = 200,
    BadRequest = 400,
    NotFound = 404,
    TooManyRequests = 429,
    ServerError = 500
}