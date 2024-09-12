using Microsoft.EntityFrameworkCore;
using P01_SalesDatabase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace 	P01_SalesDatabase.Data
{
    public class ApplicationDbContext : DbContext

    {
        public DbSet<Customer> customers {  get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Sale> sales { get; set; }
        public DbSet<Store> stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // customize Name property in Product
            modelBuilder.Entity<Product>()
        .Property(b => b.Name)
        .HasColumnType("varchar(50)").IsUnicode(true);

            modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

            // customize Name and email property in customer
            modelBuilder.Entity<Customer>(
               eb =>
               {
                   eb.Property(b => b.Name).HasColumnType("varchar(100)").IsUnicode(true);
                   eb.Property(b => b.Email).HasColumnType("varchar(80)").IsUnicode(false);
               }
                );

            // customize Name  in store
            modelBuilder.Entity<Store>()
      .Property(b => b.Name)
      .HasColumnType("varchar(80)").IsUnicode(true);

            // for sales and its relations
            modelBuilder.Entity<Sale>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.SaleId);

                // Foreign Keys
                // product
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.Sales)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade); // Cascade
                // customer
                entity.HasOne(e => e.Customer)
                      .WithMany(c => c.Sales)
                      .HasForeignKey(e => e.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade); //  Cascade
                // store
                entity.HasOne(e => e.Store)
                      .WithMany(s => s.Sales)
                      .HasForeignKey(e => e.StoreId)
                      .OnDelete(DeleteBehavior.Cascade); //  Cascade
            });



        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\MYFIRSTSQLSERVER;User Id=sa;
Password=6339782;Database=Sales;ConnectRetryCount=0;TrustServerCertificate=True");
        }
    }
}
