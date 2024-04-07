using LoginSystemJWTAuth.Models;

namespace LoginSystemJWTAuth.ServiceContracts
{
    public interface IAuthService
    {
        User AddUser(User user);
        string Login(LoginRequest request);
        Role AddRole(Role role);
        bool AssignRoleToUser(AddUserRole obj);
        IList<Role> GetAvailableRoles();
    }
}
