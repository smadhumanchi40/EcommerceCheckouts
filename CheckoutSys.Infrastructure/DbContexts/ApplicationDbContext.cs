using AspNetCoreHero.Abstractions.Domain;
using CheckoutSys.Application.Interfaces.Contexts;
using CheckoutSys.Application.Interfaces.Shared;
using CheckoutSys.Domain.Entities.Catalog;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using CheckoutSys.Domain.Entities.Discount;

namespace CheckoutSys.Infrastructure.DbContexts
{
    public class ApplicationDbContext : AuditableContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        public IDbConnection Connection => Database.GetDbConnection();

        public bool HasChanges => ChangeTracker.HasChanges();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            if (_authenticatedUser.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_authenticatedUser.UserId);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            
            base.OnModelCreating(builder);
            builder.Seed();
        }

    }
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory { Id = 1, Name = "Fruits", ParentCategoryId = 0 });
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Description = "Apple", Name = "Apple", Barcode = "barcode", CreatedBy = "ck", CreatedOn = DateTime.UtcNow,IsDeleted = false, IsPublished = true, NotReturnable = true, OrderMaximumQuantity = 200, OrderMinimumQuantity = 0, Rate = 0.6m, StockQuantity = 200},
                new Product { Id = 2, Description = "Orange", Name = "Orange", Barcode = "barcode", CreatedBy = "ck", CreatedOn = DateTime.UtcNow,IsDeleted = false, IsPublished = true, NotReturnable = true, OrderMaximumQuantity = 200, OrderMinimumQuantity = 0, Rate = 0.25m, StockQuantity = 200}
            );
            modelBuilder.Entity<Discount>().HasData(
                new Discount { Id = 1, Name = "buy 1 get 1 free", DiscountTypeId = 2, CreatedBy = "ck", CreatedOn = DateTime.UtcNow, EntityId = 1, UsePercentage = true, DiscountPercentage = 100, DiscountAmount = 0, MaximumDiscountAmount = 10000, StartDateUtc = DateTime.UtcNow, EndDateUtc = DateTime.UtcNow.AddDays(30), DiscountLimitationId = 0, LimitationTimes = 0, IsCumulative = true, IsProductBundleEnabled = true,BundleSelectionProductId = 1, BundleSelectionQuantity = 2, BundleSelectionOperation = "MultipleOf" },
                new Discount { Id = 2, Name = "3 for the price of 2 on Oranges", DiscountTypeId = 2, CreatedBy = "ck", CreatedOn = DateTime.UtcNow, EntityId = 2, UsePercentage = true, DiscountPercentage = 100, DiscountAmount = 0, MaximumDiscountAmount = 10000, StartDateUtc = DateTime.UtcNow, EndDateUtc = DateTime.UtcNow.AddDays(30), DiscountLimitationId = 0, LimitationTimes = 0, IsCumulative = true, IsProductBundleEnabled = true, BundleSelectionProductId = 2, BundleSelectionQuantity = 3, BundleSelectionOperation = "MultipleOf" }
            );
        }
    }
}