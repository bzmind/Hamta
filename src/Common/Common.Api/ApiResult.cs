namespace Common.Api;

public class ApiResult
{
    public bool IsSuccessful { get; set; }
    public MetaData MetaData { get; set; }
}

public class ApiResult<TData>
{
    public bool IsSuccessful { get; set; }
    public TData? Data { get; set; }
    public MetaData MetaData { get; set; }
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
    UnAuthorize = 401,
    NotFound = 404,
    ServerError = 500
}