using System.Net;
using AutoMapper;
using Common.Api;
using Common.Api.Utility;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Questions;
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
    private readonly IMapper _mapper;

    public QuestionController(IQuestionFacade questionFacade, IMapper mapper)
    {
        _questionFacade = questionFacade;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateQuestionCommandViewModel viewModel)
    {
        var command = _mapper.Map<CreateQuestionCommand>(viewModel);
        command.UserId = User.GetUserId();
        var result = await _questionFacade.Create(command);
        var resultUrl = Url.Action("Create", "Question", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("SetStatus")]
    public async Task<ApiResult> SetStatus(SetQuestionStatusCommand command)
    {
        var result = await _questionFacade.SetStatus(command);
        return CommandResult(result);
    }

    [HttpPut("AddReply")]
    public async Task<ApiResult> AddReply(AddReplyCommandViewModel viewModel)
    {
        var command = _mapper.Map<AddReplyCommand>(viewModel);
        command.UserId = User.GetUserId();
        var result = await _questionFacade.AddReply(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveReply")]
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

    [HttpGet("GetById/{questionId}")]
    public async Task<ApiResult<QuestionDto?>> GetById(long questionId)
    {
        var result = await _questionFacade.GetById(questionId);
        return QueryResult(result);
    }

    [HttpGet("GetByFilter")]
    public async Task<ApiResult<QuestionFilterResult>> GetByFilter([FromQuery] QuestionFilterParam filterParams)
    {
        var result = await _questionFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}