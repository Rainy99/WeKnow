using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeKnow.Models;

namespace WeKnow.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }
		[HttpPost]
		public ActionResult Index(string UserName, string Userpwd) {
			if(UserName == null || Userpwd == null) {
				Response.Write("<script>alert('请输入用户名或密码！')</script>");
			}
			else {
				using(WeizhiModel weizhi = new WeizhiModel()) {
					if(ModelState.IsValid) {
						var users = weizhi.UserInfo.FirstOrDefault(u => u.UserName == UserName && u.UserPwd == Userpwd);
						var uid = weizhi.UserInfo.FirstOrDefault(id => id.UserName == UserName).UserID;
						if(users == null) {
							Response.Write("<script>alert('用户名或密码错误！请重试！')</script>");
						}
						else {
                            HttpCookie cookie = new HttpCookie("uid", uid.ToString());
                            cookie.Expires = DateTime.MaxValue;
                            Response.SetCookie(cookie);
                            //Response.Cookies.Remove("uid");//清除
							Response.Write("<script>alert('登录成功！是否跳转')</script>");
						}
					}
				}
			}

			return View();
		}
    }
}
