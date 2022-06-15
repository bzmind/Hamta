using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shop.Presentation.Facade.Users.Tokens;

namespace Common.Api.Jwt;

public class CustomJwtValidation
{
    private readonly IUserTokenFacade _userTokenFacade;

    public CustomJwtValidation(IUserTokenFacade userTokenFacade)
    {
        _userTokenFacade = userTokenFacade;
    }

    public async Task Validate(TokenValidatedContext context)
    {
        var jwtToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        var token = await _userTokenFacade.GetTokenByJwtTokenHash(jwtToken);

        if (token == null)
            context.Fail(ValidationMessages.FieldNotFound("توکن"));
    }
}