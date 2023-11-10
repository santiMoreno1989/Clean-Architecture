using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(cfg => 
{
	cfg.UseSqlServerStorage("Data Source=LOST-LEGACY\\SQLEXPRESS;Initial Catalog=HangfireTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
});
builder.Services.AddHangfireServer(options=> 
{
	options.WorkerCount = 30;
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseHangfireDashboard();

app.Run();
