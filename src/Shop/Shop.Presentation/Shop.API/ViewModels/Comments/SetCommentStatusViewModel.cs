using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;
using Shop.Domain.CommentAggregate;

namespace Shop.API.ViewModels.Comments;

public class SetCommentStatusViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long CommentId { get; set; }

    [DisplayName("وضعیت نظر")]
    [Required(ErrorMessage = ValidationMessages.CommentStatusRequired)]
    [EnumNotNull(ErrorMessage = ValidationMessages.InvalidCommentStatus)]
    public Comment.CommentStatus Status { get; set; }
}