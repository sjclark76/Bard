using Microsoft.EntityFrameworkCore;

namespace Fluent.Testing.Sample.Api
{
    public class BankAccount
    {
        public int? Id { get; set; }
        
        public string? CustomerName { get; set; }
    }

    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options)
            : base(options)
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; } = null!;
    }
}