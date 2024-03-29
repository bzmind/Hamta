using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Shop.UI.Setup;
using Shop.UI.Setup.Middleware;
using Shop.UI.Setup.ModelStateExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddRazorRuntimeCompilation().AddMvcOptions(options =>
{
    options.Filters.Add<SerializeModelStateFilter>();
}).AddRazorPagesOptions(options =>
{
    options.Conventions.AuthorizeFolder("/Profile", "ProfileAuth");
    options.Conventions.AuthorizeFolder("/Seller", "SellerAuth");
    options.Conventions.AuthorizeFolder("/Admin", "AdminAuth");
});

builder.Services.RegisterUiDependencies();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ProfileAuth", config =>
    {
        config.RequireAuthenticatedUser();
    });
    options.AddPolicy("SellerAuth", config =>
    {
        config.RequireAuthenticatedUser();
        config.RequireAssertion(authContext => authContext.User.Claims
            .Any(claim => claim.Type == ClaimTypes.Role && claim.Value.ToLower().Contains("seller")));
    });
    options.AddPolicy("AdminAuth", config =>
    {
        config.RequireAuthenticatedUser();
        config.RequireAssertion(authContext => authContext.User.Claims
            .Any(claim => claim.Type == ClaimTypes.Role && claim.Value.ToLower().Contains("admin")));
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SignInKey"])),
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true
    };
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/ServerError");
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["Token"]?.ToString();
    if (!string.IsNullOrWhiteSpace(token))
    {
        context.Request.Headers.Append("Authorization", $"Bearer {token}");
    }
    await next();
});

app.Use(async (context, next) =>
{
    await next();
    var status = context.Response.StatusCode;
    if (status == 401)
    {
        var previousPath = context.Request.Path;
        context.Response.Redirect($"/login?redirectTo={previousPath}");
    }
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseUiCustomExceptionHandler();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();