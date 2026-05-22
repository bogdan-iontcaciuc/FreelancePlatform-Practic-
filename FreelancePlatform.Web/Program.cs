
using Blazored.LocalStorage;
using FreelancePlatform.Web.Components;
using Microsoft.AspNetCore.Components.Web;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 🔥 AICI TREBUIE HttpClient
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7020/")
    });

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AnuntService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<ApplicationService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<ReviewService>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseStaticFiles();
app.MapRazorComponents<App>()

    .AddInteractiveServerRenderMode();

app.Run();