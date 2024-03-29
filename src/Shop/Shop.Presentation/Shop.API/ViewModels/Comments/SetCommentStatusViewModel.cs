﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using Shop.Domain.CommentAggregate;

namespace Shop.API.ViewModels.Comments;

public class SetCommentStatusViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseComment)]
    public long Id { get; set; }

    [DisplayName("وضعیت نظر")]
    [Required(ErrorMessage = ValidationMessages.CommentStatusRequired)]
    public Comment.CommentStatus Status { get; set; }
}