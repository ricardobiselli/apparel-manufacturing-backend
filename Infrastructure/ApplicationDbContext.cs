using Domain.Models;
using Domain.Models.Logs;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Garment> Garments { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<GarmentWorker> GarmentWorkers { get; set; }
        public DbSet<MaintenanceWorker> MaintenanceWorkers  { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<ServiceLog> ServicesLog { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>()
            .HasOne(x => x.Garment)
            .WithMany(x => x.Operations)
            .HasForeignKey(x => x.GarmentId);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.Productions)
                .WithOne(x => x.Order);

            modelBuilder.Entity<MaintenanceWorker>()
                .HasMany(x => x.ServicesLog)
                .WithOne(x => x.MaintenanceWorker);

            modelBuilder.Entity<Machine>()
                .HasMany(x => x.ServicesLog)
                .WithOne(x => x.Machine);

        }
    }
}
