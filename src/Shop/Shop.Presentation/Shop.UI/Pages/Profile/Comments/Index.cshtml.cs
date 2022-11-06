using Common.Api.Utility;
using Microsoft.AspNetCore.Mvc;
using Shop.Query.Comments._DTOs;
using Shop.UI.Services.Comments;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Profile.Comments;

public class IndexModel : BaseRazorPage
{
    private readonly ICommentService _commentService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        ICommentService commentService) : base(razorToStringRenderer)
    {
        _commentService = commentService;
    }

    public CommentFilterResult Comments { get; set; }

    public async Task OnGet()
    {
        Comments = await GetData(async () => await _commentService.GetByFilter(new CommentFilterParams()
        {
            UserId = User.GetUserId()
        }));
    }

    public async Task<IActionResult> OnPost(long commentId)
    {
        var result = await _commentService.Remove(commentId);
        MakeAlert(result);
        return AjaxReloadCurrentPageResult();
    }
}