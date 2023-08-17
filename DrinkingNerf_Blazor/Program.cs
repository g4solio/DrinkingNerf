using DrinkingNerf_DB;
using DrinkingNerf_Engine.Challenges;
using DrinkingNerf_Engine.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

ServiceConfigurator.Configure(builder.Services, builder.Configuration.GetSection("DBSettings"));

builder.Services.AddSingleton<ChallengeService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<PointSystemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
