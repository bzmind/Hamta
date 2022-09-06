using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Questions;

public class AddReplyViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseQuestion)]
    public long QuestionId { get; set; }

    [DisplayName("توضیحات")]
    [Required(ErrorMessage = ValidationMessages.DescriptionRequired)]
    [MaxLength(300, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Description { get; set; }
}