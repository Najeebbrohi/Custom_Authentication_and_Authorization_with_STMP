using System;
using System.Threading.Tasks;

namespace CustomeAuthenticationAndRolesInCodeFirst.Controllers
{
    internal class UserManager<T>
    {
        internal Task FindByIdAsync(object userId)
        {
            throw new NotImplementedException();
        }

        internal Task ChangePasswordAsync(object userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}