namespace LoginSystemJWTAuth.Models
{
    public class AddUserRole
    {
        public int UserID { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
