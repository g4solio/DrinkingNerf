using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DrinkingNerf;
using DrinkingNerf_Engine.Challenges;
using DrinkingNerf_Engine.Users;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

DrinkingNerf_DB.ServiceConfigurator.Configure(builder.Services, builder.Configuration.GetSection("DBSettings"));

builder.Services.AddTransient<ChallengeService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<PointSystemService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });



await builder.Build().RunAsync();
