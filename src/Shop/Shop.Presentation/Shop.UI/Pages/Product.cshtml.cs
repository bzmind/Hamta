using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Comments;
using Shop.Query.Comments._DTOs;
using Shop.Query.Products._DTOs;
using Shop.UI.Services.Comments;
using Shop.UI.Services.Products;
using Shop.UI.Services.Users;
using Shop.UI.Setup.ModelStateExtensions;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages;

[BindProperties]
public class ProductModel : BaseRazorPage
{
    private readonly IProductService _productService;
    private readonly ICommentService _commentService;
    private readonly IUserService _userService;

    public ProductModel(IRazorToStringRenderer razorToStringRenderer, IProductService productService,
        ICommentService commentService, IUserService userService) : base(razorToStringRenderer)
    {
        _productService = productService;
        _commentService = commentService;
        _userService = userService;
    }

    public SingleProductDto Product { get; set; }
    public CreateCommentViewModel Comment { get; set; } = new();

    public async Task OnGet(string slug)
    {
        Product = await GetData(async () => await _productService.GetSingleBySlug(slug));
    }

    public async Task<IActionResult> OnPost(string slug)
    {
        var result = await _commentService.Create(Comment);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return RedirectToPage("Product", new { slug }).WithModelStateOf(this);
        }
        MakeSuccessAlert("نظر شما ثبت شد، و پس از تایید در سایت نمایش داده خواهد شد.");
        return RedirectToPage("Product", new { slug });
    }

    public async Task<IActionResult> OnPostAddToFavorites(long productId)
    {
        var result = await _userService.AddFavoriteItem(productId);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult("/Profile/Favorites");
    }

    public async Task<IActionResult> OnGetShowComments(long productId, int pageId)
    {
        var comments = await GetData(async () => await _commentService.GetForProduct(new ProductCommentFilterParams
        {
            PageId = pageId,
            Take = 15,
            ProductId = productId
        }));
        return await AjaxHtmlSuccessResultAsync("Shared/Product/_Comments", comments);
    }

    public async Task<IActionResult> OnPostLikeComment(long commentId)
    {
        var result = await _commentService.SetLikes(commentId);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxSuccessResult();
    }

    public async Task<IActionResult> OnPostDislikeComment(long commentId)
    {
        var result = await _commentService.SetDislikes(commentId);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxSuccessResult();
    }

    public async Task<IActionResult> OnPostRemoveComment(long commentId)
    {
        var result = await _commentService.Remove(commentId);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxReloadCurrentPageResult();
    }
}