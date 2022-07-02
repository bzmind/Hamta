using Common.Api;
using Shop.Query.Questions._DTOs;
using System.Text.Json;
using Shop.API.CommandViewModels.Questions;
using Shop.Application.Questions.RemoveReply;
using Shop.Application.Questions.SetStatus;

namespace Shop.UI.Services.Questions;

public class QuestionService : IQuestionService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public QuestionService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    public async Task<ApiResult?> Create(CreateQuestionCommandViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/question/create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> SetStatus(SetQuestionStatusCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/question/setstatus", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> AddReply(AddReplyCommandViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/question/addreply", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> RemoveReply(RemoveReplyCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/question/removereply", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Remove(long questionId)
    {
        var result = await _client.DeleteAsync($"api/question/setstatus/{questionId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<QuestionDto?> GetById(long questionId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<QuestionDto>>($"api/question/getbyid/{questionId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<QuestionFilterResult?> GetByFilter(QuestionFilterParams filterParams)
    {
        var url = $"api/question/getbyfilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&ProductId={filterParams.ProductId}&UserId={filterParams.UserId}&Status={filterParams.Status}";

        var result = await _client.GetFromJsonAsync<ApiResult<QuestionFilterResult>>(url, _jsonOptions);
        return result?.Data;
    }
}