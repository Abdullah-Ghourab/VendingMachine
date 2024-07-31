using VendingMachine.Core.Models;

namespace VendingMachine.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user, string role);
    }
}
