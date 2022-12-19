using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WEB_0535005_Vashkevich.Entities;

namespace WEB_0535005_Vashkevich.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public new DbSet<ApplicationUser> Users { get; set; }
    public new DbSet<AlbumCategory> AlbumCategories { get; set; }
    public new DbSet<Album> Albums { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        builder.Entity<AlbumCategory>().ToTable("AlbumCategories");
        builder.Entity<Album>().ToTable("Albums");
        base.OnModelCreating(builder);
    }
}
