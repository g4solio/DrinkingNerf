using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DrinkingNerf;
using DrinkingNerf_Engine.Challenges;
using DrinkingNerf_Engine.Users;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Configuration.AddJsonFile("appsettings.json");

DrinkingNerf_DB.ServiceConfigurator.Configure(builder.Services, builder.Configuration.GetSection("DBSettings"));

builder.Services.AddSingleton<ChallengeService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<PointSystemService>();


await builder.Build().RunAsync();
