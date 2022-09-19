using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.Business.DTOs
{
    public class CreateUserAddressDTO
    {
        public string Address { get; set; }
        public int UserId { get; set; } 
    }
}
