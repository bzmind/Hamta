using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Comments;

public class SetCommentDislikesViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long UserId { get; set; }

    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long CommentId { get; set; }
}