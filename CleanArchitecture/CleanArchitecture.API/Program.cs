using CleanArchitecture.API;
using CleanArchitecture.API.Middlewares;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Infraestructure;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Http.Json;
using Serilog;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(opt =>
{
    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration)
.Enrich.FromLogContext()
.CreateLogger();

builder.Services.AddHttpClient<IHttpAlkemyService, HttpAlkemyService>(client => 
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiAlkemy").Value);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient<HttpSolutionService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("SolutionApiUrl").Value);
});

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddTransient<ErrorHandlerMiddleware>();

builder.Services.AddHangfire(configuration => configuration
.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
.UseSimpleAssemblyNameTypeSerializer()
.UseRecommendedSerializerSettings()
.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
{
	CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
	SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
	QueuePollInterval = TimeSpan.Zero,
	UseRecommendedIsolationLevel = true,
	DisableGlobalLocks = true
}));

builder.Services.AddHangfireServer();

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddControllers();
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

app.ConfigureMiddleware();

app.UseHttpsRedirection();

app.UseHangfireDashboard("/Dashboard");

app.MapControllers();

app.Run();
