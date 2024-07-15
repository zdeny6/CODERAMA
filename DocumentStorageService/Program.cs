using DocumentStorageService.FormatProviders;
using DocumentStorageService.Interfaces;
using DocumentStorageService.Services;

using IFormatProvider = DocumentStorageService.Interfaces.IFormatProvider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddSingleton<IDocumentStorage, InMemoryDocumentStorage>();
builder.Services.AddSingleton<IFormatProvider, JsonFormatProvider>();
builder.Services.AddSingleton<IFormatProvider, XmlFormatProvider>();
builder.Services.AddSingleton<IFormatProvider, MessagePackFormatProvider>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Due to prefered HTTP instead of HTTPS use localhost:5000, for HTTPS apply UseHttpsRedirection and use https://localhost:5001
//app.UseHttpsRedirection();
app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseRouting();
app.MapControllers();

app.Run();
