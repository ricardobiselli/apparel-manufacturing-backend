namespace Domain.Models.Users
{
    public class Worker : User
    {

        //public ICollection<OperationLog> Operations { get; set; }
        public Worker() { }
        public Worker(int employeeIdNumber, string userName, string name, string lastName, string email, string password, string userType) :
            base(employeeIdNumber, userName, name, lastName, email, password, "GarmentWorker")
        {

        }
    }
}
