namespace Usain.CommandListener.Infrastructure.Security
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IRequestAuthenticator
    {
        Task<bool> IsAuthenticAsync(
            HttpRequest request);
    }
}
