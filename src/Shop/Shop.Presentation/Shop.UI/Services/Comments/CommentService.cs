﻿using Common.Api;
using Shop.Query.Comments._DTOs;
using System.Text.Json;
using Shop.API.ViewModels.Comments;

namespace Shop.UI.Services.Comments;

public class CommentService : BaseService, ICommentService
{
    protected override string ApiEndpointName { get; set; } = "Comment";

    public CommentService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateCommentViewModel model)
    {
        return await PostAsJsonAsync("Create", model);
    }

    public async Task<ApiResult> SetStatus(SetCommentStatusViewModel model)
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

    public async Task<CommentDto?> GetById(long commentId)
    {
        var result = await GetFromJsonAsync<CommentDto>($"Remove/{commentId}");
        return result.Data;
    }

    public async Task<CommentFilterResult> GetByFilter(CommentFilterParams filterParams)
    {
        var url = $"api/comment/GetByFilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&UserId={filterParams.UserId}&ProductId={filterParams.ProductId}&Status={filterParams.Status}";
        var result = await GetFromJsonAsync<CommentFilterResult>(url);
        return result.Data;
    }
}