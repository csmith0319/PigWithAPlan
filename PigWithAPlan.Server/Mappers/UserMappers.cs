using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigWithAPlan.Server.Models;

namespace PigWithAPlan.Server.Mappers
{
    public static class UserMappers
    {
        public static UserViewModel ToUserViewModel(this User model)
        {
            return new UserViewModel
            {
                Username = model.Username
            };
        }
    }
}