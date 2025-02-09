using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class Worker : User
    {
        public Worker(string userName, string name, string lastName, string email, string password) : base(userName, name, lastName, email, password)
        {

        }
    }
}
