using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudySphere.Contexts;
using StudySphere.Models;
using System.Net;
using System.Net.Mail;

namespace StudySphere.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private UserContext _userContext;
        private UserLoginConfigs _configs;
        public ResetPasswordService(UserContext userContext, IOptions<UserLoginConfigs> configs)
        {
            _userContext = userContext;
            _configs = configs.Value;
        }
        public async Task<string> ResetPasswordLink(string email, CancellationToken cancellationToken)
        {
            await ValidateUser(email);

            var resetToken = Guid.NewGuid().ToString();

            return await SendResetEmail(email, resetToken);
        }
        public async Task<string> ResetPassword(ResetPassword request, CancellationToken cancellationToken)
        {
            var user = await _userContext.Users.Where(x => x.ResetToken == request.Token).FirstOrDefaultAsync();
            if (user == null)
                throw new Exception("Invalid token");

            if (request.NewPassword != request.ConfirmPassword)
                throw new Exception("Password and confirm password must match.");

            return await ChangePassword(request, user);           
        }

        private async Task<string> ChangePassword(ResetPassword request, Users? user)
        {
            user.Password = request.NewPassword;
            _userContext.Update(user);

            await _userContext.SaveChangesAsync();

            return "Password reset successfully.";
        }

        private async Task ValidateUser(string email)
        {
            var foundUser = await _userContext.Users.Where(x => x.Username.ToLower() == email.ToLower()).AnyAsync();

            if (!foundUser)
                throw new Exception("Username does not exist");
        }

        private async Task<string> SendResetEmail(string email, string resetToken)
        {
            //TODO: add encryption to reset token
            var user = await _userContext.Users.Where(x => x.Username == email).FirstOrDefaultAsync();
            user.ResetToken = resetToken;
            _userContext.Update(user);
            await _userContext.SaveChangesAsync();
            //TODO: move data to secrets
            var smtpClient = new SmtpClient(_configs.SmtpServer)
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_configs.AccountEmail, _configs.EmailCode),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync(_configs.AccountEmail!, email, "Reset Password", $"Hi, we received a request to reset your password follow this link: https://www.example.com/resetPassword?token={resetToken} to reset your password");
            return "Email sent.";
        }

    }
}
