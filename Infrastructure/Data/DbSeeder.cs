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
        if (context.Users.Any())
            return;

        context.Users.AddRange(
            new User
            {
                EmployeeIdNumber = "1000",
                FirstName = "Admin",
                LastName = "Demo",
                PasswordHash =
                    passwordService.HashPassword("admin123"),
                Role = UserRole.Admin,
                MustChangePassword = false,
                State = EntitiesState.Active
            },

            new User
            {
                EmployeeIdNumber = "2001",
                FirstName = "natalia",
                LastName = "natalia",
                PasswordHash =
                    passwordService.HashPassword("operator123"),
                Role = UserRole.Operator,
                MustChangePassword = false,
                State = EntitiesState.Active
            },

            new User
            {
                EmployeeIdNumber = "2002",
                FirstName = "john",
                LastName = "doe",
                PasswordHash =
                    passwordService.HashPassword("operator123"),
                Role = UserRole.Operator,
                MustChangePassword = false,
                State = EntitiesState.Active
            },

            new User
            {
                EmployeeIdNumber = "2003",
                FirstName = "juan",
                LastName = "perez",
                PasswordHash =
                    passwordService.HashPassword("operator123"),
                Role = UserRole.Operator,
                MustChangePassword = false,
                State = EntitiesState.Active
            }
        );

        context.SaveChanges();
    }
}