using System.Collections.Generic;

namespace Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        public static Dictionary<string, string> _users = new Dictionary<string, string>() {
            { "561f172e-3a69-4dfc-bac4-77bdb454be9d", "Paulo Leonardo de O. Miranda" }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string FindUsernameByToken(string token)
        {
            return _users[token];
        }
    }
}
