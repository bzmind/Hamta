using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Questions;

public class CreateQuestionViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseProduct)]
    public long ProductId { get; set; }

    [DisplayName("توضیحات")]
    [Required(ErrorMessage = ValidationMessages.DescriptionRequired)]
    [MaxLength(300, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Description { get; set; }
}