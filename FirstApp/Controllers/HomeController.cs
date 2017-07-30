using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FirstApp.Services;
using Microsoft.AspNetCore.Http;
using FirstApp.Models.Views;
using FirstApp.Models.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using FirstApp.Data.UnitOfWork;

namespace FirstApp.Controllers {
	public partial class ClaimsService // TODO
	{
		public static ClaimsPrincipal GenerateClaims(User user) {
			var claim = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

			claim.AddClaim(new Claim("UserId", user.Id.ToString()));
			claim.AddClaim(new Claim("Username", user.Username));

			return new ClaimsPrincipal(claim);
		}
	}

	[Route("/[action]")]
	public class HomeController : BaseController {
		[Route("/")]
		[Route("/index")] //There are those who use /index, and smart people.
		public ActionResult Index() {
			using(var uow = new UnitOfWork()) {
				return View(uow.UserRepository.All());
			}
		}

		[HttpGet]
		public ActionResult Login() => View();

		[HttpPost]
		public ActionResult Login(LoginModel login) {
			return Payload(() => {
				using(var uow = new UnitOfWork()) {
					UserService service = new UserService(uow);

					var user = service.Login(login);
					var claims = ClaimsService.GenerateClaims(user);

					Task task = AuthenticationManager.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims);
					task.Wait();

					return user;
				}
			});
		}

		[HttpGet]
		public ActionResult Register() => View();

		[HttpPost]
		public ActionResult Register(RegisterModel model) {
			return Payload(() => {
				using(var uow = new UnitOfWork()) {
					UserService service = new UserService(uow);

					var recaptcha = Request.Form["g-recaptcha-response"].FirstOrDefault();

					if(recaptcha == null) throw new Exception("You must complete the Recaptcha");
					if(!RecaptchaService.Validate(recaptcha, HttpContext.Connection.RemoteIpAddress.ToString())) throw new Exception("Recaptcha invalid");

					var user = service.Register(model);
					var claims = ClaimsService.GenerateClaims(user);

					Task task = AuthenticationManager.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims);
					task.Wait();

					return user;
				}
			});
		}

		[HttpPost]
		public ActionResult Logout() {
			return Payload(() => {
				if(HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated) {
					Task task = AuthenticationManager.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
					task.Wait();

					return true;
				} else throw new Exception("(You) are not currently logged in");
			});
		}

		[HttpGet]
		public ActionResult GetAllUsers() {
			return Null();
		}

		[HttpGet]
		public ActionResult FourOhFour() {
			return View();
		}
	}
}