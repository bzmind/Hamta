using System.Text.Json.Serialization;
using Common.Api;
using Common.Api.Jwt;
using Common.Api.Middleware;
using Common.Api.Utility;
using Microsoft.AspNetCore.Mvc;
using Shop.API.SetupClasses;
using Shop.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.RegisterShopDependencies(connectionString);
builder.Services.RegisterApiDependencies();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddControllers()
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
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(settings =>
{
    settings.DisplayRequestDuration();
    settings.EnableTryItOutByDefault();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseApiCustomExceptionHandler();

app.MapControllers();

app.Run();
