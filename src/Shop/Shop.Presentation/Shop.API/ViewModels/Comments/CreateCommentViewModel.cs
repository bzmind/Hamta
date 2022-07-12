using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;
using Shop.Domain.CommentAggregate;

namespace Shop.API.ViewModels.Comments;

public class CreateCommentViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public int ProductId { get; set; }

    [DisplayName("عنوان")]
    [Required(ErrorMessage = ValidationMessages.TitleRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Title { get; set; }

    [DisplayName("توضیحات")]
    [Required(ErrorMessage = ValidationMessages.DescriptionRequired)]
    [MaxLength(1500, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Description { get; set; }

    [DisplayName("نکات مثبت")]
    [ListMaxLength(10, ErrorMessage = "{0} نمی‌تواند بیشتر از 10 عدد باشد")]
    [ListMembersCharactersMinLength(3, ErrorMessage = "متن وارد شده باید حداقل ۳ کاراکتر باشد")]
    [ListMembersCharactersMaxLength(100, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public List<string>? PositivePoints { get; set; }

    [DisplayName("نکات منفی")]
    [ListMaxLength(10, ErrorMessage = "{0} نمی‌تواند بیشتر از 10 عدد باشد")]
    [ListMembersCharactersMinLength(3, ErrorMessage = "متن وارد شده باید حداقل ۳ کاراکتر باشد")]
    [ListMembersCharactersMaxLength(100, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public List<string>? NegativePoints { get; set; }

    [DisplayName("پیشنهاد")]
    [Required(ErrorMessage = ValidationMessages.CommentRecommendationRequired)]
    [EnumNotNull(ErrorMessage = ValidationMessages.InvalidCommentRecommendation)]
    public Comment.CommentRecommendation Recommendation { get; set; }
}