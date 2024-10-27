using KillBillDemoApp;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register configuration
builder.Services.Configure<KillBillConfig>(
    builder.Configuration.GetSection("KillBill")
);

// Register KillBill client
builder.Services.AddHttpClient<KillBillClient>();


builder.Services.AddTransient<KillBillAuthHandler>();

builder.Services.AddHttpClient<KillBillClient>()
    .AddHttpMessageHandler<KillBillAuthHandler>();


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
