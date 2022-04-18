global using Groceries.Models;
global using Groceries.Models.ViewModels;
global using Groceries.Repositories;

using Groceries.Infrastructure;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(
    o => o.UseSqlite("Data Source=./data/datadb.sqlite")
);

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IRepository<Product>, EFProductRepository>();
builder.Services.AddTransient<IRepository<Category>, EFCategoryRepository>();
builder.Services.AddScoped<Cart, SessionedCart>();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseStatusCodePages();
    app.UseDeveloperExceptionPage();
}

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}"
);

app.SeedDatabase();

app.Run();
