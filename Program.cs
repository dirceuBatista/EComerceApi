using System.Collections.Immutable;
using EComerceApi.Data;
using EComerceApi.Service.ControllerSevice;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();           
builder.Services.AddControllers();


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ProductService>();
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}


app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
