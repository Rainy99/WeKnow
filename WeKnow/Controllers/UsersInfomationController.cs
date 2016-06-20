using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeKnow.Common;
using WeKnow.Models;

namespace WeKnow.Controllers
{
	public class UsersInfomationController : Controller
	{
		//
		// GET: /UsersInfomation/

		public ActionResult UsersInfomation()
		{
			WeizhiModel weknow = new WeizhiModel();

			//查询基本资料
            var UserInfo = weknow.UserInfo.Find(3);
            //昵称
            ViewBag.Name = UserInfo.UserNick;
            //简介
            ViewBag.Desc = UserInfo.UserDesc;
            //所在地
         
            //个人签名
            ViewBag.Summary = UserInfo.Summary;
            //博客地址
            ViewBag.Blog = UserInfo.InterAdress;
		
			//查询微博
            var weibo = weknow.PublishInfo.Where(w => w.UseID == 10).Select(w =>  w.Pu_Content ).ToList();
			ViewBag.weibo = weibo;
			return View();
		}

	}
}
