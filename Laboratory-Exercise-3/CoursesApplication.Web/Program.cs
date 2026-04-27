using CoursesApplication.Domain.Models;
using CoursesApplication.Repository;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Implementation;
using CoursesApplication.Service.Interface;
using CoursesApplication.Service.Jobs;
using CoursesApplication.Web.Mapper;
using EventsManagement.Repository.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(connectionString);
    options.UseLazyLoadingProxies();
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<CoursesApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ISemesterService, SemesterService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<ITeachingService, TeachingService>();
builder.Services.AddScoped<IExamSlotService, ExamSlotService>();

builder.Services.AddScoped<CourseMapper>();
builder.Services.AddScoped<SemesterMapper>();
builder.Services.AddScoped<EnrollmentMapper>();
builder.Services.AddScoped<TeachingMapper>();
builder.Services.AddScoped<ExamSlotMapper>();

// QUARTZ JOB ------------------------------------------------------------------------------------
builder.Services.AddQuartz(options =>
{
    var jobKey = new JobKey("enrollment-deletion", "maintenance");
    options.AddJob<QuartzDeleteEnrollmentsJob>(o => o.WithIdentity(jobKey));

    options.AddTrigger(o =>
    {
        o.ForJob(jobKey).WithIdentity("enrollment-deletion-trigger")
            .WithCronSchedule("0 0/1 * * * ?")
            .WithDescription("Deletes enrollments older than 15 days and not paid");
    });
});

builder.Services.AddQuartzHostedService();
// QUARTZ JOB ------------------------------------------------------------------------------------ ^^^

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<CoursesApplicationUser>>();
        
        // Повикување на твојата метода
        await DbInitializer.SeedAsync(context, userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

app.Run();