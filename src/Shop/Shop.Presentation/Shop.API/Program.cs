using Common.Api.Middleware;
using Shop.API;
using Shop.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.RegisterShopDependencies(connectionString);
builder.Services.RegisterApiDependencies();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.UseApiCustomExceptionHandler();

app.MapControllers();

app.Run();
