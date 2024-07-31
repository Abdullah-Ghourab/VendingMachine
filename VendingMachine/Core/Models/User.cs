using Microsoft.AspNetCore.Identity;

namespace VendingMachine.Core.Models
{
    public class User : IdentityUser
    {
        public double Deposit { get; set; }
    }
}
