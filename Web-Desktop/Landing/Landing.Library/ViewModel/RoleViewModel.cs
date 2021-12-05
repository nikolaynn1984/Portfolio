using System.Collections.Generic;

namespace Landing.Library.ViewModel
{
    public class RoleViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<string> AllRole { get; set; }
        public IList<string> UserRoles { get; set; }
        public RoleViewModel()
        {
            AllRole = new List<string>();
            UserRoles = new List<string>();
        }
    }
}
