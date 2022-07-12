using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Comments;

public class RemoveCommentViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long CommentId { get; set; }
}