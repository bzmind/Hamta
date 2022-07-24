using AutoMapper;
using Common.Api;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Roles;
using Shop.Query.Roles._DTOs;
using Shop.UI.Services.Roles;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Roles;

public class IndexModel : BaseRazorPage
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public IndexModel(IRoleService roleService,
        IRazorToStringRenderer razorToStringRenderer, IMapper mapper) : base(razorToStringRenderer)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    public List<RoleDto> Roles { get; set; }

    public async Task OnGet()
    {
        Roles = await _roleService.GetAll();
    }

    public async Task<IActionResult> OnPost(CreateRoleViewModel viewModel)
    {
        var result = await _roleService.Create(viewModel);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }

        return AjaxRedirectToPageResult();
    }

    public async Task<IActionResult> OnGetShowAddPage()
    {
        return await AjaxSuccessHtmlResultAsync("_Add", new CreateRoleViewModel());
    }

    public async Task<IActionResult> OnPostEditRole(EditRoleViewModel model)
    {
        var result = await _roleService.Edit(model);
        if (result.IsSuccessful == false)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult();
    }

    public async Task<IActionResult> OnGetShowEditPage(long roleId)
    {
        var role = await _roleService.GetById(roleId);
        if (role == null)
        {
            MakeAlert(ValidationMessages.FieldNotFound("نقش"));
            return AjaxErrorMessageResult(ValidationMessages.FieldNotFound("نقش"), ApiStatusCode.NotFound);
        }

        var model = _mapper.Map<EditRoleViewModel>(role);
        return await AjaxSuccessHtmlResultAsync("_Edit", model);
    }

    public async Task<IActionResult> OnPostRemoveRole(long roleId)
    {
        var result = await _roleService.Remove(roleId);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult();
    }
}