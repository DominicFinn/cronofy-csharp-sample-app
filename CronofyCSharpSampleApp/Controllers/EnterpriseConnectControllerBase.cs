﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cronofy;

namespace CronofyCSharpSampleApp.Controllers
{
    public class EnterpriseConnectControllerBase : Controller
    {
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var cronofyCookie = Request.Cookies.Get(CronofyHelper.EnterpriseConnectCookieName);

			if (cronofyCookie == null || !CronofyHelper.LoadUser(cronofyCookie.Value, true))
			{
				filterContext.Result = new RedirectResult("/login/enterpriseconnect");
			}

			base.OnActionExecuting(filterContext);
		}

		protected override void OnException(ExceptionContext filterContext)
		{
			var ex = filterContext.Exception;

			if (ex is CronofyResponseException)
			{
				Response.Cookies.Remove(CronofyHelper.EnterpriseConnectCookieName);
				filterContext.Result = new RedirectResult("/login/enterpriseconnect");
			}

			base.OnException(filterContext);
		}
    }
}
