using Microsoft.EntityFrameworkCore;
using StudySphere.Contexts;
using StudySphere.Models;
using System.Net;
using System.Net.Mail;

namespace StudySphere.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private UserContext _userContext;
        public ResetPasswordService(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<string> ResetPasswordLink(string email, CancellationToken cancellationToken)
        {
            await ValidateUser(email);
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("calebwalls@gmail.com", "fqqu sdil oyux mpwd"),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync("calebwalls@gmail.com", email, "Reset Password", "Hi, we received a request to reset your password follow this link: https://www.facebook.com/ to reset your password");
            return "Email sent.";
        }
        //public async Task<string> ResetPassword(CreateUser user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        private async Task ValidateUser(string email)
        {
            var foundUser = await _userContext.Users.Where(x => x.Username.ToLower() == email.ToLower()).AnyAsync();
            if (foundUser)
                throw new Exception("Username already exists.");
        }

    }
}
