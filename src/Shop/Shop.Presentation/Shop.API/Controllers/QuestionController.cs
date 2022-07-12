using Common.Api;
using Common.Api.Attributes;
using Common.Api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Questions.AddReply;
using Shop.Application.Questions.Create;
using Shop.Application.Questions.RemoveReply;
using Shop.Application.Questions.SetStatus;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Questions;
using Shop.Query.Questions._DTOs;
using System.Net;
using AutoMapper;
using Shop.API.ViewModels.Questions;

namespace Shop.API.Controllers;

[Authorize]
public class QuestionController : BaseApiController
{
    private readonly IQuestionFacade _questionFacade;
    private readonly IMapper _mapper;

    public QuestionController(IQuestionFacade questionFacade, IMapper mapper)
    {
        _questionFacade = questionFacade;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateQuestionViewModel model)
    {
        var command = _mapper.Map<CreateQuestionCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _questionFacade.Create(command);
        var resultUrl = Url.Action("Create", "Question", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [CheckPermission(RolePermission.Permissions.QuestionManager)]
    [HttpPut("SetStatus")]
    public async Task<ApiResult> SetStatus(SetQuestionStatusViewModel model)
    {
        var command = _mapper.Map<SetQuestionStatusCommand>(model);
        var result = await _questionFacade.SetStatus(command);
        return CommandResult(result);
    }

    [HttpPut("AddReply")]
    public async Task<ApiResult> AddReply(AddReplyViewModel model)
    {
        var command = _mapper.Map<AddReplyCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _questionFacade.AddReply(command);
        return CommandResult(result);
    }

    [HttpPut("RemoveReply")]
    public async Task<ApiResult> RemoveReply(RemoveReplyViewModel model)
    {
        var command = _mapper.Map<RemoveReplyCommand>(model);
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