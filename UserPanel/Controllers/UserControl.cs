using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.Business.DTOs;
using UserManager.Business.DTOs.LoginUserDTO;

namespace UserPanel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControl : ControllerBase
    {
        private UserManager.Business.UserBL _UBL;
        private readonly IConfiguration _config;
        public UserControl(IConfiguration config)
        {
            _config = config;

            _UBL = new UserManager.Business.UserBL(_config);
        }
        [HttpGet]
        [Route("getUsers")]
        public List<GetAllUserDTO> GetAllUsers()
        {
            return _UBL.GetAllUsers();
        }

        [Authorize]
        [HttpGet]
        [Route("getUser")]
        public ActionResult GetUsersById(int id)
        {
            var User = _UBL.GetUsersById(id);
            if (User == null)
            {
                return NotFound("İnvalid ID");

            }
            return Ok(User);
        }
        [Route("AddUser")]
        [HttpPost]
        public void PostUser([FromBody] CreateUserDTO users)
        {
            _UBL.postUser(users);
        }
        [HttpPut]
        [Route("UpdateUser")]
        public void updateUser(UpdateUserDTO users)
        {

            _UBL.updateUser(users);
        }
        [HttpPut("ActiveteUser")]
        public void activeteUser(ActiveteUserDTO users)
        {
            _UBL.activeteUser(users);
        }
        [Route("GetActiveUsers")]
        [HttpGet]
        public List<GetActiveUserDTO> getActiveUser()
        {

            return _UBL.getActiveUser();
        }
        [HttpDelete("DeleteUser")]
        public void DeleteUser(DeleteUserDTO users)
        {
            _UBL.DeleteUser(users);
        }
        [HttpPost("AddUserAddress")]
        public void PostUserAddress(CreateUserAddressDTO useraddress)
        {

            _UBL.PostUserAddress(useraddress);
        }

        [HttpPost("Register")]
        public void UserRegister(RegisterUserDTO users)
        {

            _UBL.UserRegister(users);
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public LoginUserResponse UserLogin(LoginUserRequest loginUserRequest)
        {
            return _UBL.UserLogin(loginUserRequest);
        }



    }
}
