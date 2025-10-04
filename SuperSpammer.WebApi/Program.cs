using SuperSpammer.Engine;
using SuperSpammer.Engine.Models;
using SuperSpammer.Infastructure;
using SuperSpammer.Storage;
using SuperSpammer.Storage.Collections;
using SuperSpammer.Storage.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<EmailCredentials>(
    builder.Configuration.GetSection("EmailCredentials"));
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IMongoRepository, MongoRepository>();
builder.Services.AddSingleton<ISmtpClientService, SmtpClientService>();
builder.Services.AddScoped<IAttendantService, AttendantService>();
builder.Services.AddScoped<ISenderRepository, SenderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Login");
    return Task.CompletedTask;
});

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();