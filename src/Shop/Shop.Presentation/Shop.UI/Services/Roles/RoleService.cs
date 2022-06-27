using Common.Api;
using Shop.Query.Roles._DTOs;
using Shop.UI.Models.Roles;
using System.Text.Json;

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

    public async Task<ApiResult?> Create(CreateRoleCommandViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/role/create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> AddPermissions(SetPermissionCommandViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/role/addpermissions", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> RemovePermissions(SetPermissionCommandViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/role/removepermissions", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> Remove(long roleId)
    {
        var result = await _client.DeleteAsync("api/role/remove");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<RoleDto?> GetById(long roleId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<RoleDto>>($"api/role/getbyid/{roleId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<List<RoleDto>?> GetAll()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<RoleDto>>>("api/role/getall", _jsonOptions);
        return result?.Data;
    }
}