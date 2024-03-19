using StudySphere.Models;

namespace StudySphere.Services.UserServices
{
    public interface ICreateUserService
    {
        Task<string> CreateUser(CreateUser user, CancellationToken cancellationToken);
    }
}
