using Common.Api;
using Shop.API.ViewModels.Comments;
using Shop.Query.Comments._DTOs;
using Shop.UI.Models.Comments;
using System.Text.Json;

namespace Shop.UI.Services.Comments;

public class CommentService : ICommentService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public CommentService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    public async Task<ApiResult?> Create(CreateCommentViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/comment/create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> SetStatus(SetCommentStatusViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/comment/setstatus", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> SetLikes(long commentId)
    {
        var result = await _client.PutAsync($"api/comment/setlikes/{commentId}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> SetDislikes(long commentId)
    {
        var result = await _client.PutAsync($"api/comment/setdislikes/{commentId}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Remove(long commentId)
    {
        var result = await _client.DeleteAsync($"api/comment/remove/{commentId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<CommentDto?> GetById(long commentId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<CommentDto>>($"api/comment/remove/{commentId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<CommentFilterResult?> GetByFilter(CommentFilterParams filterParams)
    {
        var url = $"api/comment/getbyfilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&UserId={filterParams.UserId}&ProductId={filterParams.ProductId}&Status={filterParams.Status}";

        var result = await _client.GetFromJsonAsync<ApiResult<CommentFilterResult>>(url, _jsonOptions);
        return result?.Data;
    }
}