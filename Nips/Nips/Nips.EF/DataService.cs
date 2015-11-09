using Nips.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Nips.DAL
{
    public class DataService : DbContext 
    {
        public DataService()
            : base("name=Nips")
            
        {
            Database.CreateIfNotExists();
        }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Produkter> Produkter { get; set; }
        public DbSet<OrderLiner> OrderLiner { get; set; }
        public DbSet<Ordere> Ordere { get; set; }
        public DbSet<Kunder> Kunder { get; set; }
        public DbSet<Spørsmålene> Spørsmålene { get; set; }
        public DbSet<Poststeder> Poststeder { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
