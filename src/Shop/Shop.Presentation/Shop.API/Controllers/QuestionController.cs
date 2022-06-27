using Common.Api;
using Common.Api.Attributes;
using Common.Api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Questions;
using Shop.Application.Questions.AddReply;
using Shop.Application.Questions.Create;
using Shop.Application.Questions.RemoveReply;
using Shop.Application.Questions.SetStatus;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Questions;
using Shop.Query.Questions._DTOs;
using System.Net;

namespace Shop.API.Controllers;

[Authorize]
public class QuestionController : BaseApiController
{
    private readonly IQuestionFacade _questionFacade;

    public QuestionController(IQuestionFacade questionFacade)
    {
        _questionFacade = questionFacade;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateQuestionCommandViewModel model)
    {
        var command = new CreateQuestionCommand(User.GetUserId(), model.ProductId, model.Description);
        var result = await _questionFacade.Create(command);
        var resultUrl = Url.Action("Create", "Question", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [CheckPermission(RolePermission.Permissions.QuestionManager)]
    [HttpPut("SetStatus")]
    public async Task<ApiResult> SetStatus(SetQuestionStatusCommand command)
    {
        var result = await _questionFacade.SetStatus(command);
        return CommandResult(result);
    }

    [HttpPut("AddReply")]
    public async Task<ApiResult> AddReply(AddReplyCommandViewModel model)
    {
        var command = new AddReplyCommand(User.GetUserId(), model.QuestionId, model.Description);
        var result = await _questionFacade.AddReply(command);
        return CommandResult(result);
    }

    [HttpPut("RemoveReply")]
    public async Task<ApiResult> RemoveReply(RemoveReplyCommand command)
    {
        var result = await _questionFacade.RemoveReply(command);
        return CommandResult(result);
    }

    [HttpDelete("Remove/{questionId}")]
    public async Task<ApiResult> Remove(long questionId)
    {
        var result = await _questionFacade.Remove(questionId);
        return CommandResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetById/{questionId}")]
    public async Task<ApiResult<QuestionDto?>> GetById(long questionId)
    {
        var result = await _questionFacade.GetById(questionId);
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetByFilter")]
    public async Task<ApiResult<QuestionFilterResult>> GetByFilter([FromQuery] QuestionFilterParams filterParams)
    {
        var result = await _questionFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}