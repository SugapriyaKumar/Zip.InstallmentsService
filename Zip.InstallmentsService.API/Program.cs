using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Zip.installmentsService.Repo.Contracts;
using Zip.InstallmentsService;
using Zip.InstallmentsService.Contracts;
using Zip.nstallmentsService.Repo;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddScoped<IPaymentPlanFactory, PaymentPlanFactory>();
    builder.Services.AddDbContext<InstallmentsDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(InstallmentsDbContext).Assembly.FullName)), ServiceLifetime.Transient);
    builder.Services.AddScoped<IInstallmentDbContext>(provider => provider.GetService<InstallmentsDbContext>());
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //logging setup
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "App terminated.");
    throw;
}
finally
{
    LogManager.Shutdown();
}