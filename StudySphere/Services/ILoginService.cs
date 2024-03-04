﻿using Microsoft.AspNetCore.Mvc;

namespace StudySphere.Services
{
    public interface ILoginService
    {
        Task<IActionResult> Login(string username, string password, CancellationToken cancellationToken);
    }
}
