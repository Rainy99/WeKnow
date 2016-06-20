using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeKnow.Models;

namespace WeKnow.Controllers {
	public class UserInfoController : Controller {
		//
		// GET: /UserInfo/

		public ActionResult Index() {
			return View();
		}
		[HttpPost]
		public ActionResult UserInfoForm(UserInfoModel model) {
			//接受登录id
			int uid = int.Parse(Request.Cookies["uid"].Value);
			if(ModelState.IsValid) {
				ViewBag.model = model.UserName + "--" + model.ReallyUserName + "--" + model.city
				+ "--" + model.password + "--" + model.confirmPassword + "--" + model.gender + "--" + model.birthday
				+ "--" + model.KeiKnow + "--" + model.email + "--" + model.QQname;
				//截取省市区字符串
				//spit根据字符串分别截取对应数据数组
				string[] result = model.city.Split(new char[] { '/' });
				//省
				string uprovince = result[0];
				////市
				string ucity = result[1];
				//////区
				string uarea = result[2];
				using(WeizhiModel userinfoTable = new WeizhiModel()) {
					//添加到数据库
					var uInfo = userinfoTable.UserInfo.FirstOrDefault(u => u.UserID == uid);
					uInfo.UserName = model.UserName;
					uInfo.UserReallyName = model.ReallyUserName;
					//查询省市编号
					//使用原始SQL查询
					DbSet<UserInfo> ds = userinfoTable.Set<UserInfo>();
					List<UserInfo> UserInfo = ds.SqlQuery("SELECT [CityID],c.[ProvinceID] FROM [dbo].[City] c join [dbo].[Province] p on c.ProvinceID=p.ProvinceID where "
					+ "c.CityName='" + ucity + "' AND p.ProvinceName='" + uprovince + "'").ToList();
					//将查询的省市编号修改到对应用户
					uInfo.CityID = int.Parse(UserInfo.Select(c => c.CityID).ToString().ToString());
					uInfo.ProvinceID = int.Parse(UserInfo.Select(c => c.ProvinceID).ToString());
					ViewBag.data = uInfo.CityID + uInfo.ProvinceID;
					uInfo.UserPwd = model.password;
					uInfo.Sexid = model.gender;
					uInfo.Brithday = model.birthday;
					uInfo.InterAdress = model.KeiKnow;
					uInfo.UserName = model.email;
					uInfo.QQ = model.QQname;
					//保存更改
					userinfoTable.SaveChanges();
					Response.Write("<script>alert('修改成功！')</script>");
				}
			}
			return View("Index");
		}

		[HttpPost]
		public ActionResult UserPwdForm() {

			return View();
		}
	}
}
