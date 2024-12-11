using Microsoft.EntityFrameworkCore;

namespace CRUD_Productos.Shared
{
    public partial class SQLDBContext : DbContext
    {
        private readonly string _conexionString = "Server=DESKTOP-JTOV11F\\SQLEXPRESS;Database=crud;Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False;";
        public SQLDBContext()
        {
                
        }

        public SQLDBContext(DbContextOptions<SQLDBContext> options) : base(options) 
        {
            
        }

        public virtual DbSet<Product> Product { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_conexionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
