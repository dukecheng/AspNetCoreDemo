using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreDemo.ShareWebFramework.Sessions
{
    public interface IUserSession
    {
        /// <summary>
        /// 当前请求是否已通过身份认证
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// 当前登录用户的显示名称
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// UserId
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// User Email
        /// </summary>
        string Email { get; }
    }
}
