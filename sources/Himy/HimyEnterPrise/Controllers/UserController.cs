using Himy.Models;
using Himy.Models.Accounts;
using Himy.Services.Application;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HimyEnterPrise.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {
            _logger = new Logger();
        }

        /// <summary>
        /// Managerを指定してインスタンスを初期化します。
        /// </summary>
        /// <param name="userManager">ユーザーマネージャを指定します。</param>
        /// <param name="signInManager">サインインマネージャを指定します。</param>
        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <summary>
        /// インデックスを返します。
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ログイン画面を返します。
        /// </summary>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// ユーザー新規作成画面を返します。
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// ログイン処理を行います。
        /// </summary>
        /// <param name="user">ユーザーモデルを指定します。</param>
        /// <returns>ログイン結果を返します。</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            AccountResultData result = new AccountResultData();
            try
            {
                User target = await UserManager.FindAsync(user.UserName, user.Password);
                if ((target != null) && (target.IsValid == true))
                {
                    await SignInManager.SignInAsync(target, false, false);

                    result.MinUser.UserName = target.UserName;
                    result.MinUser.MailAddress = target.MailAddress;
                    result.MinUser.Id = target.Id;

                    result.IsResult = true;
                }
                else
                {
                    result.ResultMessage = "ユーザー名、もしくはパスワードが間違っています。";
                }
            }
            catch(Exception e)
            {
                result.ResultMessage = "エラーが発生しました。";
                _logger.WriteLog(e);
            }

            return Json(result);
        }

        /// <summary>
        /// ユーザー新規作成処理を行います。
        /// </summary>
        /// <param name="user">ユーザーモデルを指定します。</param>
        /// <returns>成功時はViewを、失敗時は結果をJsonで返します。</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            AccountResultData accountResult = new AccountResultData();
            try
            {
                user.Id = Guid.NewGuid().ToString();
                user.DateCreated = DateTime.Now;
                user.LastUpdated = DateTime.Now;
                user.IsValid = true;

                IdentityResult result = await UserManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {
                    User target = await UserManager.FindByNameAsync(user.UserName);
                    if (target != null)
                    {
                        await SignInManager.SignInAsync(target, isPersistent: false, rememberBrowser: false);

                        accountResult.IsResult = true;
                    }
                }
                else
                {
                    accountResult.ResultMessage = "アカウントの作成に失敗しました";
                }
            }
            catch (Exception e)
            {
                _logger.WriteLog(e);
            }

            return Json(accountResult);
        }

        /// <summary>
        /// URLを指定し、表示しているページにてリダイレクトします。
        /// </summary>
        /// <param name="returnUrl">URLを指定します。</param>
        /// <returns>RedirectToActionを返します。</returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {

                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /// <summary> ユーザーマネージャです。 </summary>
        private ApplicationUserManager _userManager;

        /// <summary> サインインマネージャです。 </summary>
        private ApplicationSignInManager _signInManager;

        private Logger _logger;
    }
}