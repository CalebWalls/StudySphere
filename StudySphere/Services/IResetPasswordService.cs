using StudySphere.Models;

namespace StudySphere.Services
{
    public interface IResetPasswordService
    {
        Task<string> ResetPasswordLink(string email, CancellationToken cancellationToken);
        Task<string> ResetPassword(ResetPassword request, CancellationToken cancellationToken);
    }
}
