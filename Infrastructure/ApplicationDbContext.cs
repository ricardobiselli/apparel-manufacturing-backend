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
        public DbSet<CutBatch> CutBatches { get; set; }
        public DbSet<CutBatchSize> CutBatchSizes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<BundleSize> BundleSizes { get; set; }
        public DbSet<OrderGarmentSize> OrderGarmentSizes { get; set; }



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

            modelBuilder.Entity<CutBatch>()
                .HasOne(cb => cb.Order)
                .WithMany(o => o.CutBatches)
                .HasForeignKey(cb => cb.OrderId);

            modelBuilder.Entity<CutBatch>()
                .HasOne(cb => cb.Garment)
                .WithMany()
                .HasForeignKey(cb => cb.GarmentId);

            modelBuilder.Entity<CutBatchSize>()
                .HasKey(cbs => new
                {
                    cbs.CutBatchId,
                    cbs.SizeId
                });

            modelBuilder.Entity<CutBatchSize>()
                .HasOne(cbs => cbs.CutBatch)
                .WithMany(cb => cb.Sizes)
                .HasForeignKey(cbs => cbs.CutBatchId);

            modelBuilder.Entity<CutBatchSize>()
                .HasOne(cbs => cbs.Size)
                .WithMany(s => s.CutBatchSizes)
                .HasForeignKey(cbs => cbs.SizeId);

            modelBuilder.Entity<Bundle>()
                .HasOne(b => b.CutBatch)
                .WithMany(cb => cb.Bundles)
                .HasForeignKey(b => b.CutBatchId);

            modelBuilder.Entity<BundleSize>()
                .HasKey(bs => new
                {
                    bs.BundleId,
                    bs.SizeId
                });

            modelBuilder.Entity<BundleSize>()
                .HasOne(bs => bs.Bundle)
                .WithMany(b => b.Sizes)
                .HasForeignKey(bs => bs.BundleId);

            modelBuilder.Entity<BundleSize>()
                .HasOne(bs => bs.Size)
                .WithMany(s => s.BundleSizes)
                .HasForeignKey(bs => bs.SizeId);


            modelBuilder.Entity<MachineSession>()
                .HasOne(ms => ms.Bundle)
                .WithMany(b => b.MachineSessions)
                .HasForeignKey(ms => ms.BundleId)
                .IsRequired(false);

            modelBuilder.Entity<OrderGarmentSize>()
                .HasKey(ogs => new
                {
                    ogs.OrderId,
                    ogs.GarmentId,
                    ogs.SizeId
                });

            modelBuilder.Entity<OrderGarmentSize>()
                .HasOne(ogs => ogs.OrderGarment)
                .WithMany(og => og.Sizes)
                .HasForeignKey(ogs => new
                {
                    ogs.OrderId,
                    ogs.GarmentId
                });

            modelBuilder.Entity<OrderGarmentSize>()
                .HasOne(ogs => ogs.Size)
                .WithMany(s => s.OrderGarmentSizes)
                .HasForeignKey(ogs => ogs.SizeId);
        }
    }
}
