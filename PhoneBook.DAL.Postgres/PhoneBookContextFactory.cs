using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PhoneBook.DAL;

public class PhoneBookContextFactory : IDesignTimeDbContextFactory<PhoneBookContext>
{
    public PhoneBookContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
#if DEBUG
        var connectionString = config.GetConnectionString("TestConnection");
#elif RELEASE
        var connectionString = config.GetConnectionString("ProductionConnection");
#endif

        return new PhoneBookContext(connectionString);
    }
    
    private string[] args = [];
    public PhoneBookContext CreateDbContext()
    {
        return CreateDbContext(args);
    }
    
}