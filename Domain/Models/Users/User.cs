using Domain.Enums;

namespace Domain.Models.Users
{
    public abstract class User
    {
        public int UserId { get; set; }
        public int EmployeeIdNumber { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }

        public EntitiesState State { get; set; } = EntitiesState.Active;
        public User() { }
        public User(int employeeIdNumber, string userName, string name, string lastName, string email, string password, string userType)
        {
            EmployeeIdNumber = employeeIdNumber;
            UserName = userName;
            FirstName = name;
            LastName = lastName;
            Email = email;
            Password = password;
            UserType = userType;
        }

    }
}
