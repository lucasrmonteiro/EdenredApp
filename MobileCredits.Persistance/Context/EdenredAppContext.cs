using System.ComponentModel.DataAnnotations;
using MobileCredits.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace MobileCredits.Persistance.Context;

public class EdenredAppContext  : DbContext
{
    public EdenredAppContext(DbContextOptions<EdenredAppContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Beneficiary> Beneficiaries { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Verified)
                .IsRequired();

            entity.HasMany(u => u.Beneficiaries)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId); 
        });

        modelBuilder.Entity<Beneficiary>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NickName).IsRequired();
            entity.HasMany(b => b.BalanceAed)
                .WithOne(b => b.Beneficiary)
                .HasForeignKey(b => b.BeneficiaryId);
        });
       
        modelBuilder.Entity<BalanceAED>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).IsRequired();
        });
        
    }
    public override int SaveChanges()
    {
        ValidateEntities();
        return base.SaveChanges();
    }

    private void ValidateEntities()
    {
        var entities = from e in ChangeTracker.Entries()
            where e.State == EntityState.Added || e.State == EntityState.Modified
            select e.Entity;

        foreach (var entity in entities)
        {
            var validationContext = new ValidationContext(entity);
            Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
        }
    }
    
}