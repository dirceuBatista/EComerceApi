
using System.Text;
using EComerceApi;
using EComerceApi.Data;
using EComerceApi.InputModel;
using EComerceApi.Models;
using EComerceApi.Service.ControllerSevice;
using EComerceApi.Service.TokenService;
using EComerceApi.Validators;
using EComerceApi.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();

var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
AuthenticationService();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();           
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
ServiceController();
ValidatorService();


var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void ServiceController()
{
    builder.Services.AddTransient<TokenService>();
    builder.Services.AddScoped<ProductService>();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<OrderService>();
    builder.Services.AddScoped<AccountService>();
}

void ValidatorService()
{
    builder.Services.AddScoped<IValidator<UserViewModel>, UserValidator>();
    builder.Services.AddScoped<IValidator<RegisterViewModel>, RegisterValidator>();
    builder.Services.AddScoped<IValidator<ProductViewModel>, ProductValidator>();
    builder.Services.AddScoped<IValidator<OrderInputModel>, OrderInputModelValidator>();
    builder.Services.AddScoped<IValidator<OrderItemViewModel>, OrderItemValidator>(); 
}

void AuthenticationService()
{
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}