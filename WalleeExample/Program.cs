using System.Net;
using Wallee.Client;
using Wallee.Service;
using WalleeExample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IExampleService, ExampleService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();

// Wallee Settings
var walleCfg = new Configuration(
    builder.Configuration.GetValue<string>("Wallee:AppUserId"), 
    builder.Configuration.GetValue<string>("Wallee:AuthKey"));
walleCfg.ApiClient.RestClient.Options.Proxy = new WebProxy
{
    Address = new Uri(builder.Configuration.GetValue<string>("Proxy:Url")),
    BypassProxyOnLocal = builder.Configuration.GetValue<bool>("Proxy:LocalhostBypass")
};
builder.Services.AddSingleton(walleCfg);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();