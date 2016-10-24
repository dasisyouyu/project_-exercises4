using Himy.Models;
using Himy.Models.Accounts;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Himy.Services.Application
{
    /// <summary>
    /// ユーザー認証を行う機能を実装したサービスクラスです。
    /// </summary>
    class UserStoreService :
       IUserStore<User>,
        IUserStore<User, string>,
        IUserPasswordStore<User, string>
    {
        /// <summary>
        /// 初期化処理です。
        /// </summary>
        /// <param name="context">コンテキストを指定します。</param>
        public UserStoreService(HContext context)
        {
            Context = context;
            _logger = new Logger();
        }

        /// <summary>
        /// ユーザーを作成します。
        /// </summary>
        /// <param name="user">ユーザーオブジェクト</param>
        /// <returns>IdentityResult オブジェクト</returns>
        public Task CreateAsync(User user)
        {
            try
            {
                Context.Users.Add(user);
                Context.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.WriteLog(e);
            }

            return Task.FromResult(default(object));
        }

        /// <summary>
        /// ユーザーを削除します。
        /// </summary>
        /// <param name="user">ユーザーオブジェクト</param>
        /// <returns>IdentityResult オブジェクト</returns>
        public Task DeleteAsync(User user)
        {
            User target = (from t in Context.Users
                           where t.Id == user.Id
                           select t).FirstOrDefault();

            Context.Users.Remove(target);
            Context.SaveChanges();

            return Task.FromResult(default(object));
        }

        /// <summary>
        /// ユーザーを Id を指定して取得します。
        /// </summary>
        /// <param name="userId">ユーザー ID</param>
        /// <returns>ユーザーオブジェクト</returns>
        public Task<User> FindByIdAsync(string userId)
        {
            User ret = (from t in Context.Users
                        where t.Id == userId
                        select t).FirstOrDefault();

            return Task.FromResult(ret);
        }

        /// <summary>
        /// ユーザーをユーザー名を指定して取得します。
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <returns>ユーザーオブジェクト</returns>
        public Task<User> FindByNameAsync(string userName)
        {
            User user = null;
            try
            {
                user = (from t in Context.Users
                        where t.UserName == userName
                        select t).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.WriteLog(e);
            }

            return Task.FromResult(user);
        }

        /// <summary>
        /// ユーザー情報を更新します。
        /// </summary>
        /// <param name="user">ユーザーオブジェクト</param>
        /// <returns>IdentityResult オブジェクト</returns>
        public Task UpdateAsync(User user)
        {
            User target = (from t in Context.Users
                           where t.Id == user.Id
                           select t).FirstOrDefault();
            target.UserName = user.UserName;
            target.MailAddress = user.MailAddress;
            Context.SaveChanges();

            return Task.FromResult(default(object));
        }

        /// <summary>
        /// ユーザーにハッシュ化されたパスワードを設定します。
        /// </summary>
        /// <param name="user">ユーザーオブジェクト</param>
        /// <param name="passwordHash">パスワード文字列(未暗号化?)</param>
        /// <returns>IdentityResult オブジェクト</returns>
        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.Password = passwordHash;

            return Task.FromResult(default(object));
        }

        /// <summary>
        /// ユーザーからパスワードのハッシュを取得する
        /// </summary>
        /// <param name="user">ユーザーオブジェクト</param>
        /// <returns>パスワードハッシュ文字列</returns>
        public Task<string> GetPasswordHashAsync(User user)
        {
            var passwordHash = Context.Users.Find(user.Id).Password;

            return Task.FromResult(passwordHash);
        }

        /// <summary>
        /// パスワードが設定されている場合に true を返却します。
        /// </summary>
        /// <param name="user">ユーザーオブジェクト</param>
        /// <returns>パスワードが設定されている場合は true、それ以外の場合は false</returns>
        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(user.Password != null);
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }
        }

        /// <summary> DBContextです。 </summary>
        private HContext Context;

        private Logger _logger;
    }
}
