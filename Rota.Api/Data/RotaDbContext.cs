using Microsoft.EntityFrameworkCore;
using Rota.Api.Domain;

namespace Rota.Api.Data
{
    public class RotaDbContext : DbContext
    {
        public RotaDbContext(DbContextOptions<RotaDbContext> options)
            : base(options)
        {
        }

        #region DBSETS
        public DbSet<RouteRequest> RouteRequests { get; set; }
        public DbSet<Waypoint> Waypoints { get; set; }
        public DbSet<RouteResult> RouteResults { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        #endregion

        #region CONFIGURAÇÃO
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region RouteRequest → Waypoints
            modelBuilder.Entity<RouteRequest>()
                .HasMany(r => r.Waypoints)
                .WithOne(w => w.RouteRequest)
                .HasForeignKey(w => w.RouteRequestId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region RouteRequest → RouteResults
            modelBuilder.Entity<RouteRequest>()
                .HasMany(r => r.Results)
                .WithOne(rr => rr.RouteRequest)
                .HasForeignKey(rr => rr.RouteRequestId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region RouteRequest → Vehicle
            modelBuilder.Entity<RouteRequest>()
                .HasOne(r => r.Vehicle)
                .WithMany(v => v.RouteRequests)
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.SetNull);
            #endregion

            #region Waypoints
            modelBuilder.Entity<Waypoint>()
                .Property(w => w.Order)
                .IsRequired();
            #endregion

            #region RouteResult
            modelBuilder.Entity<RouteResult>()
                .Property(r => r.SerializedPath)
                .HasColumnType("nvarchar(max)");
            #endregion
        }
        #endregion
    }
}
