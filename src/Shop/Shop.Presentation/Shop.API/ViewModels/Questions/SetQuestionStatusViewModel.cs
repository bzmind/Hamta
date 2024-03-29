﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using Shop.Domain.QuestionAggregate;

namespace Shop.API.ViewModels.Questions;

public class SetQuestionStatusViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseQuestion)]
    public long QuestionId { get; set; }

    [DisplayName("وضعیت سوال")]
    [Required(ErrorMessage = ValidationMessages.QuestionStatusRequired)]
    public Question.QuestionStatus Status { get; set; }
}