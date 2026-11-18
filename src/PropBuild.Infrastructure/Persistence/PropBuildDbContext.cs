using Microsoft.EntityFrameworkCore;
using PropBuild.Domain.Entities;

namespace PropBuild.Infrastructure.Persistence;

/// <summary>
/// EF Core persistence boundary. Entity sets are intentionally deferred until business modules are approved.
/// </summary>
public sealed class PropBuildDbContext(DbContextOptions<PropBuildDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(b => { b.ToTable("users"); b.HasKey(x => x.Id); b.Property(x => x.Email).HasMaxLength(320).IsRequired(); b.Property(x => x.NormalizedEmail).HasMaxLength(320).IsRequired(); b.HasIndex(x => x.NormalizedEmail).IsUnique(); b.Property(x => x.DisplayName).HasMaxLength(200).IsRequired(); b.Property(x => x.PasswordHash).IsRequired(); });
        modelBuilder.Entity<Organization>(b => { b.ToTable("organizations"); b.HasKey(x => x.Id); b.Property(x => x.Name).HasMaxLength(200).IsRequired(); b.Property(x => x.TimeZone).HasMaxLength(100).IsRequired(); b.Property(x => x.Currency).HasMaxLength(3).IsFixedLength().IsRequired(); });
        modelBuilder.Entity<Membership>(b => { b.ToTable("memberships"); b.HasKey(x => new { x.OrganizationId, x.UserId }); b.Property(x => x.Role).HasConversion<string>().HasMaxLength(20).IsRequired(); b.HasOne(x => x.Organization).WithMany(x => x.Memberships).HasForeignKey(x => x.OrganizationId).OnDelete(DeleteBehavior.Restrict); b.HasOne(x => x.User).WithMany(x => x.Memberships).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict); });
        modelBuilder.Entity<RefreshToken>(b => { b.ToTable("refresh_tokens"); b.HasKey(x => x.Id); b.Property(x => x.TokenHash).HasMaxLength(64).IsRequired(); b.HasIndex(x => x.TokenHash).IsUnique(); b.HasIndex(x => new { x.UserId, x.ExpiresAt }); b.HasOne(x => x.User).WithMany(x => x.RefreshTokens).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict); });
    }
}
