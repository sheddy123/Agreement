using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Agreement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> tbl_Products { get; set; }
        public DbSet<ProductGroup> tbl_ProductGroups { get; set; }
        public DbSet<Agreement> tbl_Agreements { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>()
                .HasIndex(u => u.ProductNumber)
                .IsUnique();

            builder.Entity<ProductGroup>()
                .HasIndex(u => u.GroupCode)
                .IsUnique();

            //builder.Entity<Agreement>()
            //    .HasIndex(u => u.UserId)
            //    .IsUnique();
            builder.Entity<Agreement>()
    .HasOne(e => e.Product)
    .WithMany()
    .OnDelete(DeleteBehavior.Restrict);
            


        }
    }
}