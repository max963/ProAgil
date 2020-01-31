using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProAgil.WebAPI.IdentityDomain;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, 
        UserRole, IdentityUserLogin<int>,  IdentityRoleClaim<int>, IdentityUserToken<int>> 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Palestrante> Palestrante { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>(u => u.HasKey(ur => new { ur.UserId, ur.RoleId}));
            
            modelBuilder.Entity<User>(u => 
                u.HasMany(u => u.UeserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired()
            );

            modelBuilder.Entity<Role>(u => 
                u.HasMany(u => u.UeserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired()
            );

            modelBuilder.Entity<PalestranteEvento>().HasKey(p => new { p.EventoId, p.PalestranteId });
        }
    }
}