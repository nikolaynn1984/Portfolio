﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Server.Intarfase
{
    interface IRolesHandler
    {
        Task<IActionResult> Creaat(string name);
        Task<IActionResult> UserList();
        Task<IActionResult> GetUserById(string userid);
        Task<IActionResult> Edit(string userid, List<string> roles);

    }
}
