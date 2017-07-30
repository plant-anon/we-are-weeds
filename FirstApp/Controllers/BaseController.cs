using FirstApp.Models.Abstract;
using FirstApp.Models.Entities;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace FirstApp.Controllers {
	public class BaseController : Controller {
		protected ActionResult Payload<T>(Func<T> data) {
			JsonPayload<T> result = new JsonPayload<T>();
			try {
				result.Payload = data();
			} catch(Exception ex) {
				result.SetException(ex);
			}

			return Json(result);
		}

		protected ActionResult Null() {
			return Json(new JsonPayload());
		}

		public AuthenticationManager AuthenticationManager {
			get {
				return HttpContext.Authentication;
			}
		}

		public User CurrentUser {
			get {
				User user = new User();
				ClaimsPrincipal principal = User as ClaimsPrincipal;

				if(principal != null) {
					foreach(Claim claim in principal.Claims) {

					}
				}

				return user;
			}
		}
	}
}
