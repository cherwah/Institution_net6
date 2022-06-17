/*
Project created for Visual Studio Code via:
    - mkdir Institution_net6
    - cd Institution_net6
    - dotnet new mvc --framework "net6.0"

Required Packages:
    - dotnet add package Microsoft.EntityFrameworkCore --version 6.0.6
    - dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.6
    - dotnet add package Microsoft.EntityFrameworkCore.Proxies --version 6.0.6    
*/

using Institution_net6.DB;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// add database context into DI container
// database connection string, db_conn, found in 'appsettings.json'
builder.Services.AddDbContext<DBContext>(opt =>
    opt.UseLazyLoadingProxies().UseSqlServer(
        builder.Configuration.GetConnectionString("db_conn"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (IServiceScope scope = app.Services.CreateScope()) {
    DBContext context = scope.ServiceProvider.GetRequiredService<DBContext>();

    DBManager dbManager = new DBManager(context);

    // creates the database if it does not exists;
    // if you want a new database to be created after making
    // changes to your models, manually delete the database
    // first before running the program
    if (! context.Database.CanConnect())
    {
        // ensure that database has been created
        // before moving pass this line
        context.Database.EnsureCreated();

        // seeding mock-up data into the database
        dbManager.Seed();
    }

    // perform queries and update
    // output will go to your "debug console"
    dbManager.ListTeachDays("Kim", "Tan");
    dbManager.ListModulesWithNoClasses();
    dbManager.ListStudentsWithAtLeastOneClass();
    dbManager.UpdateClassFee(2, 800);
}

app.Run();




