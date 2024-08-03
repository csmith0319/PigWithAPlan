using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigWithAPlan.Server.Dtos.User;
using PigWithAPlan.Server.Models;

namespace PigWithAPlan.Server.Mappers
{
    public static class UserMappers
    {
        public static UserDTO ToUserDTO(this User model)
        {
            return new UserDTO
            {
                Username = model.Username
            };
        }
    }
}