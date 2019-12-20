﻿using System.Web.Mvc;

namespace ShopDemo.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "DangNhap", controller = "UserAdmin", id = UrlParameter.Optional }
            );
        }
    }
}