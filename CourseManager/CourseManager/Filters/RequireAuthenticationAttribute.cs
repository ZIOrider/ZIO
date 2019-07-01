using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseManager.Models.ValidatableObjects;
using CourseManager.Models;

namespace CourseManager.Filters
{
    public class RequireAuthenticationAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                //TODO
                var Suser = filterContext.HttpContext.Session["user"];
                var user = "";
                if (Suser == null)
                {
                    user = " ";
                }
                else
                {
                    user = Suser.ToString();
                }
                if (!string.IsNullOrWhiteSpace(user))
                {
                    return;
                }

                var cookie = filterContext.HttpContext.Request.Cookies["user"];
                var temp = "";
                if (cookie == null)
                {
                    temp = " ";
                }
                else
                {
                    temp = cookie.ToString();
                }
                if (string.IsNullOrWhiteSpace(temp))
                {
                    throw new UnauthorizedException();
                }
             
                var content = temp.DecryptQueryString();
                CourseManagerEntities db = new CourseManagerEntities();
                if (!db.Users.Any(u => u.Account == content))
                {
                    throw new UnauthorizedException();
                }

            }
        }
    }
}