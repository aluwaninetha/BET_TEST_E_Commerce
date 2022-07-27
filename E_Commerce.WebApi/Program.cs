using E_Commerce.Application;
using E_Commerce.Application.Interfaces;
using E_Commerce.Identity;
using E_Commerce.Infrastructure.Persistence;
using E_Commerce.Shared;
using E_Commerce.WebApi.Extensions;
using E_Commerce.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager _config = builder.Configuration;

builder.Services.AddApplicationLayer();
builder.Services.AddIdentityInfrastructure(_config);
builder.Services.AddPersistenceInfrastructure(_config);
builder.Services.AddSharedInfrastructure(_config);
builder.Services.AddSwaggerExtension();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddApiVersioningExtension();
builder.Services.AddHealthChecks();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseErrorHandlingMiddleware();
app.UseHealthChecks("/health");

app.MapControllers();

app.Run();
