using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landing.Model.ViewModel
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
