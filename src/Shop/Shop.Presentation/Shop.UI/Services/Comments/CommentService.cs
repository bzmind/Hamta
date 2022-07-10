using Common.Api;
using Shop.Query.Comments._DTOs;
using System.Text.Json;
using Shop.API.CommandViewModels.Comments;
using Shop.Application.Comments.SetStatus;

namespace Shop.UI.Services.Comments;

public class CommentService : BaseService, ICommentService
{
    protected override string ApiEndpointName { get; set; } = "Comment";

    public CommentService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateCommentCommandViewModel model)
    {
        return await PostAsJsonAsync("Create", model);
    }

    public async Task<ApiResult> SetStatus(SetCommentStatusCommand model)
    {
        return await PutAsJsonAsync("SetStatus", model);
    }

    public async Task<ApiResult> SetLikes(long commentId)
    {
        return await PutAsync($"SetLikes/{commentId}");
    }

    public async Task<ApiResult> SetDislikes(long commentId)
    {
        return await PutAsync($"SetDislikes/{commentId}");
    }

    public async Task<ApiResult> Remove(long commentId)
    {
        return await DeleteAsync($"Remove/{commentId}");
    }

    public async Task<CommentDto> GetById(long commentId)
    {
        var result = await GetFromJsonAsync<CommentDto>($"Remove/{commentId}");
        return result.Data;
    }

    public async Task<CommentFilterResult> GetByFilter(CommentFilterParams filterParams)
    {
        var url = $"api/comment/GetByFilterPageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&UserId={filterParams.UserId}&ProductId={filterParams.ProductId}&Status={filterParams.Status}";
        var result = await GetFromJsonAsync<CommentFilterResult>(url);
        return result.Data;
    }
}