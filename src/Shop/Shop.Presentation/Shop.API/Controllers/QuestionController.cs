using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Questions.AddReply;
using Shop.Application.Questions.Create;
using Shop.Application.Questions.RemoveReply;
using Shop.Application.Questions.SetStatus;
using Shop.Presentation.Facade.Questions;
using Shop.Query.Questions._DTOs;

namespace Shop.API.Controllers;

public class QuestionController : BaseApiController
{
    private readonly IQuestionFacade _questionFacade;

    public QuestionController(IQuestionFacade questionFacade)
    {
        _questionFacade = questionFacade;
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create(CreateQuestionCommand command)
    {
        var result = await _questionFacade.Create(command);
        var resultUrl = Url.Action("Create", "Question", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("AddReply")]
    public async Task<ApiResult> AddReply(AddReplyCommand command)
    {
        var result = await _questionFacade.AddReply(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveReply")]
    public async Task<ApiResult> RemoveReply(RemoveReplyCommand command)
    {
        var result = await _questionFacade.RemoveReply(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> SetStatus(SetQuestionStatusCommand command)
    {
        var result = await _questionFacade.SetStatus(command);
        return CommandResult(result);
    }

    [HttpDelete]
    public async Task<ApiResult> Remove(RemoveReplyCommand command)
    {
        var result = await _questionFacade.Remove(command);
        return CommandResult(result);
    }

    [HttpGet("{questionId}")]
    public async Task<ApiResult<QuestionDto?>> GetById(long questionId)
    {
        var result = await _questionFacade.GetById(questionId);
        return QueryResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<QuestionFilterResult>> GetByFilter([FromQuery] QuestionFilterParam filterParams)
    {
        var result = await _questionFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}