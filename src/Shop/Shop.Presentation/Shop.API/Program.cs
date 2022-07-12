using Common.Api;
using Common.Api.Jwt;
using Common.Api.Middleware;
using Common.Api.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Shop.Config;
using System.Text.Json.Serialization;
using Shop.API.Setup;

var builder = WebApplication.CreateBuilder(args);
const string CORSPolicyName = "ApiCORS";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(CORSPolicyName, policy =>
    {
        policy.WithOrigins("https://localhost:7212");
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.RegisterShopDependencies(connectionString);
builder.Services.RegisterApiDependencies(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var result = new ApiResult
            {
                IsSuccessful = false,
                MetaData = new MetaData
                {
                    ApiStatusCode = ApiStatusCode.BadRequest,
                    Message = ModelStateUtility.CollectModelStateErrors(context.ModelState)
                }
            };
            return new BadRequestObjectResult(result);
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Enter Token",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

var app = builder.Build();

app.UseMiddleware<CustomIpRateLimitMiddleware>();

app.UseSwagger();

app.UseSwaggerUI(settings =>
{
    settings.DisplayRequestDuration();
    settings.EnableTryItOutByDefault();
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(CORSPolicyName);

app.UseAuthentication();

app.UseAuthorization();

app.UseApiCustomExceptionHandler();

app.MapControllers();

app.Run();