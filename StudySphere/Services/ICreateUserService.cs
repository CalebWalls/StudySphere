using StudySphere.Models;

namespace StudySphere.Services
{
    public interface ICreateUserService
    {
        Task<string> CreateUser(CreateUser user, CancellationToken cancellationToken);
    }
}
