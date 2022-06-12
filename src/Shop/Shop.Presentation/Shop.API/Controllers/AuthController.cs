using Common.Api;
using Common.Api.Jwt;
using Common.Application;
using Common.Application.Security;
using Common.Application.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Auth;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Presentation.Facade.Users;
using Shop.Presentation.Facade.Users.Tokens;
using Shop.Query.Users._DTOs;
using UAParser;

namespace Shop.API.Controllers;

public class AuthController : BaseApiController
{
    private readonly IUserFacade _userFacade;
    private readonly IUserTokenFacade _userTokenFacade;
    private readonly IConfiguration _configuration;

    public AuthController(IUserFacade userFacade, IConfiguration configuration, IUserTokenFacade userTokenFacade)
    {
        _userFacade = userFacade;
        _configuration = configuration;
        _userTokenFacade = userTokenFacade;
    }

    [HttpPost("Login")]
    public async Task<ApiResult<UserTokensDto?>> Login(LoginViewModel model)
    {
        var user = await _userFacade.GetByEmailOrPhone(model.EmailOrPhone);

        if (user == null)
            return CommandResult(OperationResult<UserTokensDto>
                .NotFound(ValidationMessages.FieldNotFound("کاربری با مشخصات وارد شده")));

        if (SHA256Hash.Compare(user.Password, model.Password) == false)
            return CommandResult(OperationResult<UserTokensDto>
                .NotFound(ValidationMessages.FieldNotFound("کاربری با مشخصات وارد شده")));

        await _userTokenFacade.RemoveTokensByUserId(user.Id); // This clears all the tokens, so the device limit sets back to zero, and user will be logged out of other devices, so it can always login from any device, idk, is it even a problem? but now the device limit doesn't make any sense I guess
        var result = await GenerateTokenAndAddItToUser(user);
        return CommandResult(result);
    }
    
    [HttpPost("Register")]
    public async Task<ApiResult> Register(RegisterViewModel model)
    {
        var result = await _userFacade.Register(new RegisterUserCommand(model.PhoneNumber, model.Password));
        return CommandResult(result);
    }

    [HttpPost("RefreshToken")]
    public async Task<ApiResult<UserTokensDto?>> RefreshToken(string refreshToken)
    {
        var result = await _userTokenFacade.GetTokenByRefreshTokenHash(refreshToken);

        if (result == null)
            return CommandResult(OperationResult<UserTokensDto?>
                .NotFound(ValidationMessages.FieldNotFound("توکن")));

        if (result.JwtTokenExpireDate > DateTime.Now)
            return CommandResult(OperationResult<UserTokensDto?>.Error("توکن هنوز منقضی نشده است"));

        if (result.RefreshTokenExpireDate < DateTime.Now)
            return CommandResult(OperationResult<UserTokensDto?>.Error("رفرش توکن منقضی شده است"));

        await _userTokenFacade.RemoveTokensByUserId(result.UserId);

        var user = await _userFacade.GetById(result.UserId);
        var userTokensResult = await GenerateTokenAndAddItToUser(user!);

        return CommandResult(userTokensResult);
    }

    [Authorize]
    [HttpPost("Logout")]
    public async Task<ApiResult> Logout()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var result = await _userTokenFacade.GetTokenByJwtTokenHash(token);

        if (result == null)
            return CommandResult(OperationResult.NotFound(ValidationMessages.FieldNotFound("توکن")));

        await _userTokenFacade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));
        return CommandResult(OperationResult.Success());
    }

    private async Task<OperationResult<UserTokensDto>> GenerateTokenAndAddItToUser(UserDto userDto)
    {
        var uapParser = Parser.GetDefault();
        var userAgentHeader = Request.Headers["user-agent"].ToString();
        var device = "not found";

        if (userAgentHeader != null)
        {
            var userAgentInfo = uapParser.Parse(userAgentHeader);
            device = $"{userAgentInfo.Device.Family} ({userAgentInfo.OS.Family} {userAgentInfo.OS.Major}" +
                     $".{(string.IsNullOrWhiteSpace(userAgentInfo.OS.Minor) ? 0 : userAgentInfo.OS.Minor)}) " +
                     $"- {userAgentInfo.UA.Family} ({userAgentInfo.UA.Major}" +
                     $".{(string.IsNullOrWhiteSpace(userAgentInfo.UA.Minor) ? 0 : userAgentInfo.UA.Minor)})";
        }

        var token = JwtTokenBuilder.BuildToken(userDto, _configuration);
        var refreshToken = Guid.NewGuid().ToString();

        var result = await _userTokenFacade.AddToken(new AddUserTokenCommand(userDto.Id, token, refreshToken,
            JwtTokenBuilder.JwtTokenExpirationDate, JwtTokenBuilder.RefreshTokenExpirationDate, device));

        if (result.StatusCode != OperationStatusCode.Success)
            return OperationResult<UserTokensDto>.Error();

        return OperationResult<UserTokensDto>.Success(new UserTokensDto
        {
            Token = token,
            RefreshToken = refreshToken
        });
    }
}