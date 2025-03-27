using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;

        }

        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<VehicleTrailer> VehicleTrailers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleTrailer>()
                .HasKey(vt => new { vt.VehicleId, vt.TrailerId, vt.BegDate, vt.EndDate });

            modelBuilder.Entity<Vehicles>()
                .HasOne(v => v.VehicleType)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId);

            modelBuilder.Entity<VehicleTrailer>()
                .HasOne(vt => vt.Vehicles)
                .WithMany(v => v.VehicleTrailer)
                .HasForeignKey(vt => vt.VehicleId);

            modelBuilder.Entity<VehicleTrailer>()
                .HasOne(vt => vt.Trailer)
                .WithMany(t => t.VehicleTrailer)
                .HasForeignKey(vt => vt.TrailerId);
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }



        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var usuarioActual = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Sistema";

            foreach (var entry in ChangeTracker.Entries<BaseAuditable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.IUser = usuarioActual;
                    entry.Entity.IDate = DateTime.UtcNow;
                    entry.Entity.IComments = "Sin modificaciones.";
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UUser = usuarioActual;
                    entry.Entity.UDate = DateTime.UtcNow;
                    entry.Entity.UComments = "Registro modificado.";
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
