using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UserManager.DataAccess.Conrete;
using UserManager.Entities.Concrete;

namespace UserManager.DataAccess
{

    public class UserDal
    {
        private readonly UserDbContext _context;       

        public List<Users> GetAllUsers()
        {
            var db = new UserDbContext();
            return db.Users.ToList();
        }
        public Users GetUsersById(int id)
        {
            UserDbContext db = new UserDbContext();
            Users user = new Users();
            user = db.Users.FirstOrDefault(x => x.Id == id);
            if(user is not null)
            {
                return user;
            }
            else
            {
               throw new NullReferenceException();
            }
        }

        public Users GetUsersEMail(string email)
        {
            UserDbContext db = new UserDbContext();
            Users user = new Users();
            user = db.Users.FirstOrDefault(x => x.Email == email);
            if (user is not null)
            {
                return user;
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        public void postUser(Users user)
        {
            var db = new UserDbContext();
            db.Add(user);
            db.SaveChanges();
        }
        public void updateUser(Users users)
        {
            var db = new UserDbContext();
            db.Update(users);
            db.SaveChanges();
        }
        public Users activeteUser(Users user)
        {
            var db = new UserDbContext();                      
            db.Update(user);
            db.SaveChanges();
            return user;
        }
        public List<Users> getActiveUser()
        {
            var db =new UserDbContext();
            return db.Users.Where(x=>x.status ==Status.Active).ToList();          
        }
        public Users DeleteUser(Users user)
        {
            var db = new UserDbContext();                        
            db.Update(user);
            db.SaveChanges();
            return user;
        }
        public UserAddress PostUserAddress(UserAddress input)
        {
            var db = new UserDbContext();         
            db.Add(input);
            db.SaveChanges();
            return input;           
        }
        public void UserRegister(Users user)
        {
            var db = new UserDbContext();
            
            db.Update(user);
            db.SaveChanges();          
        }
        public Users UserLogin(Users user)
        {
            var db = new UserDbContext();
            db.Update(user);
            db.SaveChanges();
            return user;
        }            

    }

}
