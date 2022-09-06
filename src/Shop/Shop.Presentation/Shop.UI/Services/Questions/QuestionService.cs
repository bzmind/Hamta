using Common.Api;
using Shop.Query.Questions._DTOs;
using System.Text.Json;
using Shop.API.ViewModels.Questions;

namespace Shop.UI.Services.Questions;

public class QuestionService : BaseService, IQuestionService
{
    protected override string ApiEndpointName { get; set; } = "Question";

    public QuestionService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateQuestionViewModel model)
    {
        return await PostAsJsonAsync("Create", model);
    }

    public async Task<ApiResult> SetStatus(SetQuestionStatusViewModel model)
    {
        return await PutAsJsonAsync("SetStatus", model);
    }

    public async Task<ApiResult> AddReply(AddReplyViewModel model)
    {
        return await PutAsJsonAsync("AddReply", model);
    }

    public async Task<ApiResult> RemoveReply(RemoveReplyViewModel model)
    {
        return await PutAsJsonAsync("RemoveReply", model);
    }

    public async Task<ApiResult> Remove(long questionId)
    {
        return await DeleteAsync($"Remove/{questionId}");
    }

    public async Task<QuestionDto?> GetById(long questionId)
    {
        var result = await GetFromJsonAsync<QuestionDto>($"GetById/{questionId}");
        return result.Data;
    }

    public async Task<QuestionFilterResult> GetByFilter(QuestionFilterParams filterParams)
    {
        var url = $"GetByFilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&ProductId={filterParams.ProductId}&UserId={filterParams.UserId}&Status={filterParams.Status}";
        var result = await GetFromJsonAsync<QuestionFilterResult>(url);
        return result.Data;
    }
}