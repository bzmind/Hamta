﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Shop.UI.Setup.ModelStateExtensions;

public static class KeepTempDataResultExtensions
{
    public static IKeepTempDataResult WithModelStateOf(this IKeepTempDataResult actionResult, PageModel page)
    {
        if (page.ModelState.IsValid)
            return actionResult;

        var modelState = ModelStateSerializer.Serialize(page.ModelState);
        page.TempData[SerializeModelStateFilter.Key] = modelState;
        return actionResult;
    }
}