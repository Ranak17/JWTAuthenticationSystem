using LoginSystemJWTAuth.Models;
using LoginSystemJWTAuth.ServiceContracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LoginSystemJWTAuth.Services
{
    public class AuthService : IAuthService
    {
        private static IList<User> _users;
        private static IList<Role> _roles;
        private static IList<UserRole> _userRole;
        private readonly IConfiguration _configuration;

        static AuthService()
        {
            _users = new List<User>();
            _roles = new List<Role>();
            _userRole = new List<UserRole>();
        }

        public AuthService(IConfiguration config)
        {
            _configuration = config;
        }

        public Role AddRole(Role role)
        {
            _roles.Add(role);
            return role;
        }

        public User AddUser(User user)
        {
            _users.Add(user);
            return user;
        }

        public bool AssignRoleToUser(AddUserRole obj)
        {
            //TODO : Remove FirstOrDefault
            User user = _users.FirstOrDefault(s => s.Id == obj.UserID);
            if (user == null) {
                return false;
            }
            foreach(int role in obj.RoleIds)
            {
                var userRole = new UserRole();
                userRole.RoleID= role;
                userRole.UserID = user.Id;
                _userRole.Add(userRole);
            }
            return true;
        }

        public IList<Role> GetAvailableRoles()
        {
            return _roles;
        }

        public string Login(LoginRequest request)
        {
            if(request.Username!=null  && request.Password!=null)
            {
                //TODO : Remove SingleorDefault
                User user = _users.FirstOrDefault(x=>x.Username==request.Username && x.Password==request.Password);
                if (user != null)
                {
                    List<Claim> claims = new List<Claim>() {
                    new Claim("Id",user.Id.ToString()),
                    new Claim("Username",user.Username),
                    };
                    var userRoles = _userRole.Where(x=>x.UserID==user.Id).ToList();
                    var roleIds = userRoles.Select(x=>x.RoleID).ToList();
                    var roles = _roles.Where(x=> roleIds.Contains(x.Id)).ToList();
                    foreach(var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires:DateTime.UtcNow.AddMinutes(10),
                        signingCredentials:signIn
                        );
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
            }
            return "Credentials are not valid";
        }
    }
}
