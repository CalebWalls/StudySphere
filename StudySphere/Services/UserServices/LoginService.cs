﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudySphere.Contexts;
using StudySphere.Services.UserServices;

namespace StudySphere.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserContext _userContext;
        public LoginService(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<IActionResult> Login(string username, string password, CancellationToken cancellationToken)
        {
            var foundUser = await _userContext.Users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).Select(x => new { x.FirstName, x.LastName}).FirstOrDefaultAsync();

            //TODO: add lock out
            if (foundUser == null)
                return new BadRequestObjectResult("Invalid username or password");

            return new OkObjectResult($"Login Successful! Welcome back {foundUser.FirstName} {foundUser.LastName}");
        }
    }
}
