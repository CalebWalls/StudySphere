using Microsoft.AspNetCore.Mvc;

namespace StudySphere.Services.UserServices
{
    public interface ILoginService
    {
        Task<IActionResult> Login(string username, string password, CancellationToken cancellationToken);
    }
}
