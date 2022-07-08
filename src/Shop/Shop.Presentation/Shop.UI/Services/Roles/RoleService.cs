using Common.Api;
using Shop.Query.Roles._DTOs;
using System.Text.Json;
using Shop.Application.Roles.AddPermission;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.RemovePermission;

namespace Shop.UI.Services.Roles;

public class RoleService : IRoleService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public RoleService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    public async Task<ApiResult?> Create(CreateRoleCommand model)
    {
        var result = await _client.PostAsJsonAsync("api/role/Create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> AddPermissions(AddRolePermissionCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/role/AddPermissions", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> RemovePermissions(RemoveRolePermissionCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/role/RemovePermissions", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Remove(long roleId)
    {
        var result = await _client.DeleteAsync("api/role/Remove");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<RoleDto?> GetById(long roleId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<RoleDto>>($"api/role/GetById/{roleId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<List<RoleDto>> GetAll()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<RoleDto>>>("api/role/GetAll", _jsonOptions);
        return result.Data;
    }
}