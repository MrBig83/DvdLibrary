using System.Text;
using DvdLibrary.Application;
using DvdLibrary.Application.Common.Models;
using DvdLibrary.Infrastructure;
using DvdLibrary.Infrastructure.Configuration;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// API-lagret ska bara orkestrera. All affärslogik registreras från de inre lagren.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    // Definitionen gör att Swagger kan visa en Authorize-knapp för JWT.
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Description = "Ange JWT-token enligt formatet: Bearer {din token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
});

var jwtSettings = builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
    ?? throw new InvalidOperationException("JwtSettings saknas i konfigurationen.");

// Här talar vi om hur en giltig JWT ska valideras när en skyddad endpoint anropas.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Fångar vanliga fel centralt så att controllers och handlers kan hållas rena.
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var response = exception switch
        {
            ValidationException validationException => new ErrorResponse(
                "Valideringen misslyckades.",
                validationException.Errors.Select(error => error.ErrorMessage).ToList()),
            KeyNotFoundException => new ErrorResponse("Resursen kunde inte hittas."),
            _ => new ErrorResponse("Ett oväntat fel uppstod.")
        };

        context.Response.StatusCode = exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        await context.Response.WriteAsJsonAsync(response);
    });
});

if (app.Environment.IsDevelopment())
{
    // Swagger och OpenAPI ska bara exponeras i utvecklingsmiljön.
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ordningen är viktig: auth måste köras innan controllers exekveras.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
