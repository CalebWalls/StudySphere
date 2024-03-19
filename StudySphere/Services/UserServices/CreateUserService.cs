using Microsoft.EntityFrameworkCore;
using StudySphere.Contexts;
using StudySphere.Models;
using StudySphere.Services.UserServices;
using System.Text.RegularExpressions;

namespace StudySphere.Services
{
    public class CreateUserService : ICreateUserService
    {
        private UserContext _userContext;
        public CreateUserService(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<string> CreateUser(CreateUser user, CancellationToken cancellationToken)
        {
            try
            {
                await ValidateUser(user);

                return await SaveUserToDatabase(user);
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        private async Task ValidateUser(CreateUser user)
        {
            //TODO: Add a validator sevice to handle this
            var foundUser = await _userContext.Users.Where(x => x.Username.ToLower() == user.Username.ToLower()).AnyAsync();
            if(foundUser)
                throw new Exception("Username already exists.");
            if (user.Password != user.ConfirmPassword)
                throw new Exception("Password and confirm password must match.");
            var emailRegex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
            if (!emailRegex.IsMatch(user.Username))
                throw new Exception("Username must be a valid email address.");
            if (!user.Password.Any(char.IsUpper))
                throw new Exception("Password must contain at least one uppercase character.");
            if (!user.Password.Any(char.IsDigit))
                throw new Exception("Password must contain at least one number.");
            if (!user.Password.Any(ch => !char.IsLetterOrDigit(ch)))
                throw new Exception("Password must contain at least one special character.");
        }

        private async Task<string> SaveUserToDatabase(CreateUser user)
        {
            _userContext.Users.Add(new Users { Username = user.Username, Password = user.Password, FirstName = user.FirstName, LastName = user.LastName });
            await _userContext.SaveChangesAsync();
            return "User created successfully.";
        }
    }
}
