using backend_examen.API;
using Microsoft.OpenApi.Models;
using backend_examen.Repository;
using backend_examen.Application;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<VendingMachineService>();
builder.Services.AddTransient<GetBeveragesQuery>();
builder.Services.AddTransient<BuyBeverageCommand>();
builder.Services.AddSingleton<CoinRepository>();




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseSwagger();
app.UseSwaggerUI(); 

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();

app.UseCors("AllowAll");


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
