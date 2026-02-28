using Microsoft.EntityFrameworkCore;
using Wms.Web.wwwroot.data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<WmsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WmsDb")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();