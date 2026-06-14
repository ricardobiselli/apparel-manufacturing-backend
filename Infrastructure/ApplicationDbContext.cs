using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Garment> Garments { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderGarment> OrderGarments { get; set; }
        public DbSet<MachineSession> MachineSessions { get; set; }
        public DbSet<OperationLog> OperationLogs { get; set; }
        public DbSet<MachineExceptionLog> MachineExceptionLogs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>()
                 .HasOne(x => x.Garment)
                 .WithMany(x => x.Operations)
                 .HasForeignKey(x => x.GarmentId);

            modelBuilder.Entity<OrderGarment>()
                .HasKey(og => new { og.OrderId, og.GarmentId });

            modelBuilder.Entity<MachineSession>()
                .HasOne(x => x.Order)
                .WithMany(x => x.MachineSessions)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<OrderGarment>()
                .HasOne(og => og.Order)
                .WithMany(o => o.OrderGarments)
                .HasForeignKey(og => og.OrderId);

            modelBuilder.Entity<OrderGarment>()
                .HasOne(og => og.Garment)
                .WithMany(g => g.OrderGarments)
                .HasForeignKey(og => og.GarmentId);

            modelBuilder.Entity<MachineEvent>(entity =>
            {
                entity.HasKey(e => e.MachineEventId);
                entity.Property(e => e.MachineEventId).ValueGeneratedOnAdd();

                entity.HasDiscriminator<string>("EventType")
                      .HasValue<OperationLog>("OperationLog")
                      .HasValue<MachineExceptionLog>("MachineExceptionLog");
            });


        }
    }
}
