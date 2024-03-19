using Microsoft.EntityFrameworkCore;
using ProjectsTask;
using ProjectsTask.Models;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.Configure(app);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//}
app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

app.Run();
