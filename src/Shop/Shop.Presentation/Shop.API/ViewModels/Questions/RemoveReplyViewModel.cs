using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Questions;

public class RemoveReplyViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseQuestion)]
    public long QuestionId { get; set; }

    [Required(ErrorMessage = ValidationMessages.ChooseRole)]
    public long ReplyId { get; set; }
}