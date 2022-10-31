using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Comments;
using Shop.Domain.CommentAggregate;
using Shop.Query.Comments._DTOs;
using Shop.UI.Services.Comments;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Comments;

public class IndexModel : BaseRazorPage
{
    private readonly ICommentService _commentService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        ICommentService commentService) : base(razorToStringRenderer)
    {
        _commentService = commentService;
    }

    [BindProperty(SupportsGet = true)]
    public CommentFilterParams FilterParams { get; set; }
    public CommentFilterResult Comments { get; set; }

    public async Task OnGet()
    {
        Comments = await GetData(async () => await _commentService.GetByFilter(FilterParams));
    }

    public async Task<IActionResult> OnPost(long commentId)
    {
        var result = await _commentService.SetStatus(new SetCommentStatusViewModel
        {
            Id = commentId,
            Status = Comment.CommentStatus.Accepted
        });

        MakeAlert(result);

        return AjaxReloadCurrentPageResult();
    }

    public async Task<IActionResult> OnPostRejectComment(long commentId)
    {
        var result = await _commentService.SetStatus(new SetCommentStatusViewModel
        {
            Id = commentId,
            Status = Comment.CommentStatus.Rejected
        });

        MakeAlert(result);

        return AjaxReloadCurrentPageResult();
    }
}