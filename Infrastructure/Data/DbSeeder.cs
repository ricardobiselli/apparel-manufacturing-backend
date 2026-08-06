using Application.Interfaces;
using Domain.Enums;
using Domain.Models;

namespace Infrastructure.Data;
public static class DbSeeder
{
    public static void Seed(
        ApplicationDbContext context,
        IPasswordService passwordService)
    {
        if (!context.Machines.Any())
        {
            context.Machines.AddRange(
                new Machine
                {
                    PostNumber = 101,
                    MachineName = "Automatic lockstitch 1 needle",
                    MachineModel = "FIT F-21-M",
                    InstallDate = DateOnly.Parse("2026-02-15"),
                    Status = MachineStatus.Operational
                },
                new Machine
                {
                    PostNumber = 102,
                    MachineName = "4 thread Overlock",
                    MachineModel = "Sunsure SS 747",
                    InstallDate = DateOnly.Parse("2026-02-15"),
                    Status = MachineStatus.Operational
                },
                new Machine
                {
                    PostNumber = 103,
                    MachineName = "Coverstitch",
                    MachineModel = "Sunsure coverstitch multi-function",
                    InstallDate = DateOnly.Parse("2026-02-15"),
                    Status = MachineStatus.Operational
                }
            );

            context.SaveChanges();
        }


        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User
                {
                    EmployeeIdNumber = "1000",
                    FirstName = "Admin",
                    LastName = "Demo",
                    PasswordHash = passwordService.HashPassword("admin123"),
                    Role = UserRole.Admin,
                    MustChangePassword = false,
                    State = EntityState.Active
                },

                new User
                {
                    EmployeeIdNumber = "2001",
                    FirstName = "natalia",
                    LastName = "natalia",
                    PasswordHash = passwordService.HashPassword("operator123"),
                    Role = UserRole.Operator,
                    MustChangePassword = false,
                    State = EntityState.Active
                }
            );

            context.SaveChanges();
        }


        if (!context.Garments.Any())
        {
            var sweatpants = new Garment
            {
                GarmentName = "Sweatpants SAMPLE",
                GarmentDescription = "single operator test",
                State = EntityState.Active,
                Operations = new List<Operation>
                {
                    new Operation("cuff attach", "attach both cuffs", 69, 1),
                    new Operation("croatch topstich", "topstitch the croatch piece", 32, 1),
                    new Operation("front topstich", "topstitch front + front legs pieces", 40, 1),
                    new Operation("Topstitching back", "topstitch back + leg back pieces with bartacks", 40, 1),
                    new Operation("back + labels", "overlock back, attach size and composition label", 65, 1),
                    new Operation("Attach zipper", "Attach zipper for welt pocket", 120, 2),
                    new Operation("join front with lower leg part", "join front with lower leg part", 45, 1),
                    new Operation("attach croatch", "attach croatch piece", 30, 1),
                    new Operation("attach inner pocket", "attach both inner pockets pieces", 120, 1),
                    new Operation("overlock inner pockets", "overlock inner pockets", 45, 1),
                    new Operation("join front with back", "join front and back pieces", 140, 1),
                    new Operation("last croatch topstitch", "topstich the croatch/back seam with bartacks included", 40, 1),
                    new Operation("attach waistband", "attach waistband", 40, 1),
                    new Operation("attach pockets to waistband", "bartack the top pocket piece to waistband", 25, 1),
                    new Operation("waistband topstich first pass", "first pass in lockstitch machine", 35, 1),
                    new Operation("waistband second pass", "waistband second pass with interlock machine", 60, 1)
                }
            };


            var tshirt = new Garment
            {
                GarmentName = "T-shirt SAMPLE",
                GarmentDescription = "Basic T-shirt testing",
                State = EntityState.Active,
                Operations = new List<Operation>
                {
                    new Operation("join shoulders", "join shoulders", 10, 1),
                    new Operation("collar build", "build the collar", 10, 1),
                    new Operation("attach collar", "attach collar to body", 30, 1),
                    new Operation("neck tape first pass", "attach neck tape with folder", 30, 1),
                    new Operation("neck tape second pass", "neck tape topstitch", 30, 1),
                    new Operation("sleeves + sides + label", "attach sleeves, close sides and attach size label", 140, 1),
                    new Operation("collar Topstitch", "topstitch the front part of the collar", 20, 1),
                    new Operation("bottom hemming", "hem the bottom part", 30, 1),
                    new Operation("sleeves hemming", "hem both sleeves", 45, 1)
                }
            };


            context.Garments.AddRange(
                sweatpants,
                tshirt
            );

            context.SaveChanges();
        }
    }
}