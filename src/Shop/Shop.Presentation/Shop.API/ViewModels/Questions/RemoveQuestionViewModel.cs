using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Questions;

public class RemoveQuestionViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long QuestionId { get; set; }
}