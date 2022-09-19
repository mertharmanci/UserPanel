using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UserManager.Business.DTOs;
using UserManager.Business.DTOs.LoginUserDTO;
using UserManager.Business.UserHelper;
using UserManager.DataAccess.Conrete;
using UserManager.Entities.Concrete;

namespace UserManager.Business
{
    public class UserBL
    {
        private UserManager.DataAccess.UserDal _UBL;
        private readonly IConfiguration _config;
        public UserBL(IConfiguration config)
        {
            _config = config;
            _UBL = new UserManager.DataAccess.UserDal();
        }
        public List<GetAllUserDTO> GetAllUsers()
        {
            List<Users> usersList = _UBL.GetAllUsers();
            List<GetAllUserDTO> user = new List<GetAllUserDTO>();
            if (usersList != null)
            {
                foreach (var item in usersList)
                {
                    GetAllUserDTO getAllUserDTO = new GetAllUserDTO();
                    getAllUserDTO.Id = item.Id;
                    getAllUserDTO.PhoneNumber = item.PhoneNumber;
                    getAllUserDTO.Name = item.Name;
                    getAllUserDTO.SurName = item.SurName;
                    getAllUserDTO.Email = item.Email;
                    user.Add(getAllUserDTO);
                }
            }
            return user;
        }
        public GetUserByIdDTO GetUsersById(int id)
        {
            Users user = _UBL.GetUsersById(id);
            GetUserByIdDTO getUserByIdDTO = new GetUserByIdDTO();
            if (user != null)
            {
                getUserByIdDTO.Id = user.Id;
                getUserByIdDTO.PhoneNumber = user.PhoneNumber;
                getUserByIdDTO.Name = user.Name;
                getUserByIdDTO.SurName = user.SurName;
                getUserByIdDTO.Email = user.Email;
                getUserByIdDTO.status = user.status;
                _UBL.GetUsersById(id);
            }
            return getUserByIdDTO;
        }
        public void postUser(CreateUserDTO userRequest)
        {
            Users user = new Users()
            {
                Name = userRequest.Name,
                SurName = userRequest.SurName,
                PhoneNumber = userRequest.PhoneNumber,
                Email = userRequest.Email,               
                status = Status.Active
            };
            _UBL.postUser(user);
        }
        public void updateUser(UpdateUserDTO users)
        {
            Users user = _UBL.GetUsersById(users.Id);
            if (user is not null)
            {
                user.Name = users.Name;
                user.PhoneNumber = users.PhoneNumber;
                user.SurName = users.SurName;
                user.Email = users.Email;
                _UBL.updateUser(user);
            }
        }
        public void activeteUser(ActiveteUserDTO users)
        {
            Users user = _UBL.GetUsersById(users.Id);
            user.status = Status.Active;
            _UBL.activeteUser(user);                                                     
        }
        public List<GetActiveUserDTO> getActiveUser()
        {
            List<Users> usersList = _UBL.getActiveUser();
            List<GetActiveUserDTO> user = new List<GetActiveUserDTO>();
            if (usersList != null)
            {
                foreach (var item in usersList)
                {
                    GetActiveUserDTO getActiveUserDTO = new GetActiveUserDTO();
                    getActiveUserDTO.Id = item.Id;
                    getActiveUserDTO.PhoneNumber = item.PhoneNumber;
                    getActiveUserDTO.Name = item.Name;
                    getActiveUserDTO.SurName = item.SurName;
                    getActiveUserDTO.Email = item.Email;
                    getActiveUserDTO.status = item.status;
                    user.Add(getActiveUserDTO);
                }
            }
            return user;
        }
        public void DeleteUser(DeleteUserDTO users)
        {
            Users user = _UBL.GetUsersById(users.Id);            
            user.status = Status.Passive;
            _UBL.DeleteUser(user);
        }
        public void PostUserAddress(CreateUserAddressDTO input)
        {
            Users users = _UBL.GetUsersById(input.UserId);
            UserAddress userAddress = new UserAddress();
            userAddress.UserId = input.UserId;
            userAddress.Address = input.Address;
            _UBL.PostUserAddress(userAddress);
        }
        public void UserRegister(RegisterUserDTO userRequest)
        {     
            userRequest.Password = Password.hashPassword(userRequest.Password);
            Users user = new Users()                       
            {
                Name = userRequest.Name,
                SurName = userRequest.SurName,
                PhoneNumber = userRequest.PhoneNumber,
                Email = userRequest.Email,
                Password=userRequest.Password,
                status = Status.Active
            };            
            _UBL.UserRegister(user);
        }
        public LoginUserResponse UserLogin(LoginUserRequest loginUserRequest)
        {
            Users user = _UBL.GetUsersEMail(loginUserRequest.Email);
            LoginUserResponse loginUserResponse = new LoginUserResponse();           
            if (user.Email == loginUserRequest.Email)
            {
                loginUserRequest.Password = Password.hashPassword(loginUserRequest.Password);
               
                if (user.Password == loginUserRequest.Password)
                {
                    loginUserResponse.Name = user.Name;
                    loginUserResponse.SurName = user.SurName;
                    loginUserResponse.Email = user.Email;
                    loginUserResponse.Token = CreateToken(user);                   
                    _UBL.UserLogin(user);
                }              
            }
            return loginUserResponse;
        }
        public string CreateToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = (Encoding.ASCII.GetBytes(_config.GetSection("JWT:Key").Value.ToString()));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Name),
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddMinutes(20),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }       
    }    
}
    

