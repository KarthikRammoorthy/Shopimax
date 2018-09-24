using System;
using System.Collections.Generic;

namespace Shopimax.API.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string UserEmail { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}