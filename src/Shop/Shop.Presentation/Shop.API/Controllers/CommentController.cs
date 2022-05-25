﻿using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Remove;
using Shop.Application.Comments.SetDislikes;
using Shop.Application.Comments.SetLikes;
using Shop.Application.Comments.SetStatus;
using Shop.Presentation.Facade.Comments;
using Shop.Query.Comments._DTOs;
using Shop.Query.Comments.GetById;

namespace Shop.API.Controllers;

public class CommentController : BaseApiController
{
    private readonly ICommentFacade _commentFacade;

    public CommentController(ICommentFacade commentFacade)
    {
        _commentFacade = commentFacade;
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create(CreateCommentCommand command)
    {
        var result = await _commentFacade.Create(command);
        var resultUrl = Url.Action("Create", "Comment", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("SetStatus")]
    public async Task<ApiResult> SetStatus(SetCommentStatusCommand command)
    {
        var result = await _commentFacade.SetStatus(command);
        return CommandResult(result);
    }

    [HttpPut("SetLikes")]
    public async Task<ApiResult> SetLikes(SetCommentLikesCommand command)
    {
        var result = await _commentFacade.SetLikes(command);
        return CommandResult(result);
    }

    [HttpPut("SetDislikes")]
    public async Task<ApiResult> SetDislikes(SetCommentDislikesCommand command)
    {
        var result = await _commentFacade.SetDislikes(command);
        return CommandResult(result);
    }

    [HttpDelete]
    public async Task<ApiResult> Remove(RemoveCommentCommand command)
    {
        var result = await _commentFacade.Remove(command);
        return CommandResult(result);
    }

    [HttpGet("{commentId}")]
    public async Task<ApiResult<CommentDto?>> GetCommentById(long commentId)
    {
        var result = await _commentFacade.GetCommentById(commentId);
        return QueryResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<CommentFilterResult>> GetCommentByFilter(CommentFilterParams filterParams)
    {
        var result = await _commentFacade.GetCommentByFilter(filterParams);
        return QueryResult(result);
    }
}