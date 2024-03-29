﻿using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Shippings;

public class CreateShippingViewModel
{
    [Display(Name = "نام روش ارسال")]
    [Required(ErrorMessage = ValidationMessages.NameRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Name { get; set; }

    [Display(Name = "هزینه روش ارسال")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [Range(0, double.PositiveInfinity, ErrorMessage = "{0} باید بیشتر از 0 باشد")]
    public int Cost { get; set; }
}