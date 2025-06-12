using CleanAspire.Application.Common.Interfaces;
using CleanAspire.Domain.Common;
using CleanAspire.Domain.Entities;
using CleanAspire.Domain.Identities;
using CleanAspire.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CleanAspire.Infrastructure.Persistence;
/// <summary>
/// Represents the application database context.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the Tenants DbSet.
    /// </summary>
    public DbSet<Tenant> Tenants { get; set; }

    /// <summary>
    /// Gets or sets the AuditTrails DbSet.
    /// </summary>
    public DbSet<AuditTrail> AuditTrails { get; set; }

    /// <summary>
    /// Gets or sets the Products DbSet.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Gets or sets the Stocks DbSet.
    /// </summary>
    public DbSet<Stock> Stocks { get; set; }

    public DbSet<Customer> Customers { get; set; }


    public DbSet<SalesOrder> SalesOrders { get; set; }


    /// <summary>
    /// Configures the schema needed for the identity framework.
    /// </summary>
    /// <param name="builder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Configures the conventions to be used for this context.
    /// </summary>
    /// <param name="configurationBuilder">The builder being used to configure conventions for this context.</param>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<string>().HaveMaxLength(450);
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {

        // Adjust the path relative to where migrations are run
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "CleanAspire.Api");

        if (!Directory.Exists(basePath))
        {
            throw new DirectoryNotFoundException($"Base path not found: {basePath}");
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        // Find the correct base path by traversing up from the current directory
        //string currentDirectory = Directory.GetCurrentDirectory();
        //string solutionDirectory = FindSolutionRoot(currentDirectory);
        //string apiDirectory = Path.Combine(solutionDirectory, "src", "CleanAspire.Api");

        //if (!Directory.Exists(apiDirectory))
        //{
        //    throw new DirectoryNotFoundException($"API directory not found: {apiDirectory}");
        //}

        //var configuration = new ConfigurationBuilder()
        //    .SetBasePath(apiDirectory)

        //    .AddJsonFile("appsettings.json", optional: false)
        //    .Build();

        var connectionString = configuration.GetSection("DatabaseSettings")["ConnectionString"];
        var dbProvider = configuration.GetSection("DatabaseSettings")["DBProvider"];

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        if (dbProvider?.ToLower() == "sqlite")
        {
            optionsBuilder.UseSqlite(connectionString, b =>
            {
                b.MigrationsAssembly("CleanAspire.Migrators.SQLite");
            });
        }
        else
        {
            throw new Exception($"Unsupported DB Provider: {dbProvider}");
        }

        return new ApplicationDbContext(optionsBuilder.Options);
    }


    // Helper method to find the solution root directory
    private string FindSolutionRoot(string startDirectory)
    {
        // Start from the current directory and go up until we find the solution file or reach the root
        var directory = new DirectoryInfo(startDirectory);
        while (directory != null && !directory.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }

        return directory?.FullName ?? throw new DirectoryNotFoundException("Solution root directory not found");
    }

}
