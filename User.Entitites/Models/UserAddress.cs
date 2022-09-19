using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.Entities.Concrete
{
    public class UserAddress
    {        
        public int Id { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
    }
}
