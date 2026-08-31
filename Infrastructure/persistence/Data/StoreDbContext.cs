namespace persistence.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyRefrence).Assembly);
        }

        #region DbSets
        DbSet<Product> Products { get; set; }

        DbSet<ProductBrand> ProductBrands { get; set; }

        DbSet<ProductType> ProductTypes { get; set; }

        #endregion
    }
}
