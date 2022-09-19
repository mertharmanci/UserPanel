using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.Business.DTOs.LoginUserDTO
{
    public class LoginUserResponse
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }       
        public string Token { get; set; }
    }
}
