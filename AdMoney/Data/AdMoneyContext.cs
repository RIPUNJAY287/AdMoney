using Microsoft.EntityFrameworkCore;
using AdMoney.Models;
namespace AdMoney.Data
{
    public class AdMoneyContext : DbContext
    {
        public AdMoneyContext(DbContextOptions<AdMoneyContext> options) : base (options) { }
        
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!; 

        public DbSet<AssetSecurity> AssetSecurity { get; set; }
        
        public DbSet<UserModel> UserModels { get; set; }

        public DbSet<UserModelData> UserModelsData { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionOption> QuestionOptions { get; set; }

        public DbSet<AdvisorClientQues> AdvisorClientsQues { get;set; }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<RiskProfile> RiskProfiles { get; set; }
       
      /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=AdMoney;Integrated Security=True;TrustServerCertificate=True");
        }*/
    }
}
