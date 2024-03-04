using Microsoft.EntityFrameworkCore;
using StudySphere.Contexts;
using StudySphere.Models;

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

                await SaveUserToDatabase(user);

                

                return "User created successfully.";


            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        private async Task ValidateUser(CreateUser user)
        {
            var foundUser = await _userContext.Users.Where(x => x.Username.ToLower() == user.Username.ToLower()).AnyAsync();
            if(foundUser)
                throw new Exception("Username already exists.");
            if (user.Password != user.ConfirmPassword)
                throw new Exception("Password and confirm password must match.");
        }

        private async Task SaveUserToDatabase(CreateUser user)
        {
            _userContext.Users.Add(new Users { Username = user.Username, Password = user.Password });
            await _userContext.SaveChangesAsync();
        }
    }
}
