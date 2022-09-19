using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.Business.DTOs.LoginUserDTO
{
    public class LoginUserRequest

    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
