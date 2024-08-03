using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PigWithAPlan.Server.Dtos.User
{
    public class UserDTO
    {
        [Required]
        public required string Username { get; init; }
    }
}