using LoginSystemJWTAuth.Models;
using LoginSystemJWTAuth.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystemJWTAuth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private static List<Role> _roles;
        static AuthController()
        {
            _roles = new List<Role>();
        }


        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public string Login(LoginRequest request)
        {
            return _authService.Login(request);
        }

        [HttpPost]
        public User AddUser([FromBody] User user)
        {
            return _authService.AddUser(user);
        }        
        [HttpPost]
        public Role AddRole([FromBody] Role role)
        {
            return _authService.AddRole(role);
        }       
        [HttpGet]
        public IList<Role> GetAvailableRole([FromBody] Role role)
        {
            return _authService.GetAvailableRoles();
        }
       
        public bool AssignRoleToUser([FromBody] AddUserRole userRole)
        {
            return _authService.AssignRoleToUser(userRole);
        }



    }
}
