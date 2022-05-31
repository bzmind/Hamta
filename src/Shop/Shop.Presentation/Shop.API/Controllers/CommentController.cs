using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.SetDislikes;
using Shop.Application.Comments.SetLikes;
using Shop.Application.Comments.SetStatus;
using Shop.Presentation.Facade.Comments;
using Shop.Query.Comments._DTOs;

namespace Shop.API.Controllers;

public class CommentController : BaseApiController
{
    private readonly ICommentFacade _commentFacade;

    public CommentController(ICommentFacade commentFacade)
    {
        _commentFacade = commentFacade;
    }

    [HttpPost("Create")]
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

    [HttpDelete("Remove/{commentId}")]
    public async Task<ApiResult> Remove(long commentId)
    {
        var result = await _commentFacade.Remove(commentId);
        return CommandResult(result);
    }

    [HttpGet("GetById/{commentId}")]
    public async Task<ApiResult<CommentDto?>> GetById(long commentId)
    {
        var result = await _commentFacade.GetById(commentId);
        return QueryResult(result);
    }

    [HttpGet("GetByFilter")]
    public async Task<ApiResult<CommentFilterResult>> GetByFilter([FromQuery] CommentFilterParams filterParams)
    {
        var result = await _commentFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}