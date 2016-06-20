using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeKnow.Models;

namespace WeKnow.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult VCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(6);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
        [HttpPost]
        public ActionResult VCodes(ValidateCode Vcode)
        {
            if (ModelState.IsValid)
            {
                using (WeizhiModel context = new WeizhiModel())
                {
                    var SEmail = (from u in context.UserInfo
                                  where u.UserName == Vcode.UserName
                                  select u.UserID).FirstOrDefault();
                    //邮箱没问题
                    if (SEmail == null || SEmail == 0)
                    {
                        //验证码验证
                        string data = (string)Session["ValidateCode"];
                        if (data == Vcode.txtCode)
                        {
                            var inse = new UserInfo() { UserName = Vcode.UserName, UserNick = Vcode.UserNick, UserPwd = Vcode.UserPwd };
                            context.UserInfo.Add(inse);
                            context.SaveChanges();
                        }
                        else if (Vcode.txtCode == "" || Vcode.txtCode == null)
                        {
                            ViewBag.Code = "验证码不能为空";
                        }
                        else if (data != Vcode.txtCode)
                        {
                            ViewBag.Code = "验证码错误";
                        }
                    }
                    //邮箱已注册
                    else if (SEmail != null || SEmail == 1)
                    {
                        ViewBag.Email = "邮箱已注册";
                    }
                }
            }
            return View("Index");
        }
    }
}
