using System.ComponentModel.DataAnnotations.Schema;

namespace PigWithAPlan.Server.Models
{
    public class ILogin
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}