using Microsoft.AspNetCore.Authentication.Cookies;
using Hotels.DAO;
using Hotels.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Account/Login";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
    
});

builder.Services.AddScoped<IAuthService, AuthService>();

var connectionString = builder.Configuration.GetConnectionString("HotelDatabase");
builder.Services.AddScoped<IClienteDao>(provider => new ClienteDao(connectionString));
builder.Services.AddScoped<ICameraDao>(provider => new CameraDao(connectionString));
builder.Services.AddScoped<IServizioDao>(provider => new ServizioDao(connectionString));
builder.Services.AddScoped<IPrenotazioneDao>(provider => new PrenotazioneDao(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
