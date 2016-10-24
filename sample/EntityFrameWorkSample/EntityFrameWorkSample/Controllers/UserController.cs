using EntityFrameWorkSample.Models;
using EntityFrameWorkSample.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EntityFrameWorkSample.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(User user, string returnUrl)
        {
            var userForLogin = await UserManager.FindAsync(user.UserName, user.Password);
            if ((userForLogin == null) || (userForLogin.IsValid == false))
            {
                return View(user);
            }

            await SignInManager.SignInAsync(userForLogin, false, false);

            return RedirectToLocal(returnUrl);
        }


        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            //TODO リザルトモデルを作成する。（結果、ログインユーザーID）
            try
            {
                user.Id = Guid.NewGuid().ToString();
                user.DateCreated = DateTime.Now;
                user.LastUpdated = DateTime.Now;
                IdentityResult result = await UserManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {
                    User signInUser = await UserManager.FindByNameAsync(user.UserName);
                    if (signInUser != null)
                    {
                        await SignInManager.SignInAsync(signInUser, isPersistent: false, rememberBrowser: false);
                    }
                }
                else
                {
                    //ユーザー作成失敗 リザルトから詳細をメッセージに出力
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return View();
        }

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

        /// <summary>
        /// この下すべてどうにかしたい
        /// </summary>

        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

    }
}