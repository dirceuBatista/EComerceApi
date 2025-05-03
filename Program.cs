using System.Collections.Immutable;
using EComerceApi.Data;
using EComerceApi.Service.ControllerSevice;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ProductService>();
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();


app.MapControllers();

app.Run();
