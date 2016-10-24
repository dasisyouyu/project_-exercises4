using EntityFrameWorkSample.Models;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Text;
using System.Security.Cryptography;

namespace EntityFrameWorkSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            using (HContext context = new HContext())
            {
                List<User> users = (from t in context.Users
                                    where t.Id == ""
                                    select t).ToList();
            }

            string hash = GetSha256("password");

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 文字列から SHA256 のハッシュ値を取得
        /// </summary>
        private static String GetSha256(string target)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] byteValue = Encoding.UTF8.GetBytes(target);
            byte[] hashes = sha256.ComputeHash(byteValue);

            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < hashes.Length; i++)
            {
                ret.AppendFormat("{0:x2}", hashes[i]);
            }

            return ret.ToString();
        }
    }
}