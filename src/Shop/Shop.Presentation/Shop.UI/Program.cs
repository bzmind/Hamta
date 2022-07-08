using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shop.UI.SetupClasses;
using Shop.UI.SetupClasses.ModelStateExtensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddRazorRuntimeCompilation().AddMvcOptions(options =>
{
    options.Filters.Add<SerializeModelStateFilter>();
}).AddRazorPagesOptions(options =>
{
    options.Conventions.AuthorizeFolder("/Profile", "ProfileAuth");
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

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/ServerError");
    app.UseHsts();
}

//app.UseCookiePolicy(new CookiePolicyOptions { Secure = CookieSecurePolicy.Always, MinimumSameSitePolicy = SameSiteMode.Lax});

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
        context.Response.Redirect($"/auth/login?redirectTo={previousPath}");
    }
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();