using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Microsoft.Owin.Security.VanLang
{
    public class IdentityException : Exception
    {
        public IdentityResult IdentityResult { get; set; }
    }

    public static class UserManagerExtensions
    {
        public static async Task<TUser> CreateAddLoginAsync<TUser>(this UserManager<TUser> userManager, string email) where TUser : class, IUser<string>
        {
            var user = Activator.CreateInstance(typeof(TUser)) as TUser;
            user.GetType().GetProperty("Email").SetValue(user, email);
            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var apiUrl = Constants.BaseUrl + "/API/getUserInfo?email=" + email;
                // get id from api then set id to below
                var infoLogin = new UserLoginInfo(Constants.DefaultAuthenticationName, "id???");
                result = await userManager.AddLoginAsync(user.Id, infoLogin);
                if (result.Succeeded)
                    return user;
                else
                    throw new IdentityException { IdentityResult = result };
            }
            else
                throw new IdentityException { IdentityResult = result };
        }
    }
}
