//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Models
//{
//    using Domain.Models.Users;
//    using Domain.Models;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Text;
//    using System.Threading.Tasks;
//    using System.Text.Json.Serialization;

//    namespace Application.Models
//    {
//        public class AdminDTO
//        {
//            public int UserId { get; set; }
//            public string UserName { get; set; }
//            public string FirstName { get; set; }
//            public string LastName { get; set; }
//            public string Email { get; set; }
//            public string Password { get; set; }
//            [JsonIgnore]
//            public string UserType { get; set; }

//            public static AdminDTO Create(Admin admin)
//            {
//                return new AdminDTO
//                {
//                    UserId = admin.UserId,
//                    FirstName = admin.FirstName,
//                    LastName = admin.LastName,
//                    UserName = admin.UserName,
//                    Email = admin.Email,
//                    Password = admin.Password


//                };
//            }


//        }
//    }

//}
