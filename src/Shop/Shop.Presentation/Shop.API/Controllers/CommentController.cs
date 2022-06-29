using AutoMapper;
using Common.Api;
using Common.Api.Attributes;
using Common.Api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Comments;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.SetDislikes;
using Shop.Application.Comments.SetLikes;
using Shop.Application.Comments.SetStatus;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Comments;
using Shop.Query.Comments._DTOs;
using System.Net;

namespace Shop.API.Controllers;

public class CommentController : BaseApiController
{
    private readonly ICommentFacade _commentFacade;
    private readonly IMapper _mapper;

    public CommentController(ICommentFacade commentFacade, IMapper mapper)
    {
        _commentFacade = commentFacade;
        _mapper = mapper;
    }

    [CheckPermission(RolePermission.Permissions.CommentManager)]
    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateCommentViewModel model)
    {
        var command = _mapper.Map<CreateCommentCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _commentFacade.Create(command);
        var resultUrl = Url.Action("Create", "Comment", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [CheckPermission(RolePermission.Permissions.CommentManager)]
    [HttpPut("SetStatus")]
    public async Task<ApiResult> SetStatus(SetCommentStatusCommand command)
    {
        var result = await _commentFacade.SetStatus(command);
        return CommandResult(result);
    }

    [Authorize]
    [HttpPut("SetLikes/{commentId}")]
    public async Task<ApiResult> SetLikes(long commentId)
    {
        var command = new SetCommentLikesCommand(commentId, User.GetUserId());
        var result = await _commentFacade.SetLikes(command);
        return CommandResult(result);
    }

    [Authorize]
    [HttpPut("SetDislikes/{commentId}")]
    public async Task<ApiResult> SetDislikes(long commentId)
    {
        var command = new SetCommentDislikesCommand(commentId, User.GetUserId());
        var result = await _commentFacade.SetDislikes(command);
        return CommandResult(result);
    }

    [Authorize]
    [HttpDelete("Remove/{commentId}")]
    public async Task<ApiResult> Remove(long commentId)
    {
        var result = await _commentFacade.Remove(commentId);
        return CommandResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetById/{commentId}")]
    public async Task<ApiResult<CommentDto?>> GetById(long commentId)
    {
        var result = await _commentFacade.GetById(commentId);
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetByFilter")]
    public async Task<ApiResult<CommentFilterResult>> GetByFilter([FromQuery] CommentFilterParams filterParams)
    {
        var result = await _commentFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}