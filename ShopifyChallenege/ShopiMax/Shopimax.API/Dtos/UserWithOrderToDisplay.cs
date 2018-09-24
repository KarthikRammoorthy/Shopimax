using System.Collections.Generic;
using Shopimax.API.Models;

namespace Shopimax.API.Dtos
{
    public class UserWithOrderToDisplay
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public ICollection<User> Orders { get; set; }
    }
}