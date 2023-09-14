using Microsoft.EntityFrameworkCore;
using AdMoney.Models;
namespace AdMoney.Data
{
    public class AdMoneyContext : DbContext
    {
        public AdMoneyContext(DbContextOptions<AdMoneyContext> options) : base (options) { }
        
        public DbSet<User> Users { get; set; } = null!;

      /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=AdMoney;Integrated Security=True;TrustServerCertificate=True");
        }*/
    }
}
