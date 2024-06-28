using Duende.IdentityServer;
using DuendeIdentityServerTest.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DuendeIdentityServerTest.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginDto loginForm)
		{
			if (!String.IsNullOrEmpty(loginForm.Username))
			{
				var claims = new List<Claim>
				{
					new ("sub","UUID"),
					new ("name", "Bilal"),
					new ("role", "Admin")
				};

				var ci = new ClaimsIdentity(claims, "pwd", "name", "role");
				var cp = new ClaimsPrincipal(ci);

				//var user = new IdentityServerUser("UUID") { AdditionalClaims = claims };

				await HttpContext.SignInAsync(cp);

				loginForm.Redirect = "/";
				// redirect güvenli değil veya if (Url.IsLocalUrl(loginForm.Redirect))
				return LocalRedirect(loginForm.Redirect);
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();

			return Redirect("/");
		}
	}
}
