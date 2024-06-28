using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using DuendeIdentityServerTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DuendeIdentityServerTest.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ISessionManagementService _sessionManagementService;

		public HomeController(ISessionManagementService sessionManagementService)
		{
			_sessionManagementService = sessionManagementService;
		}
		public IActionResult Index()
		{
			return View();
		}

		[Authorize]
		public async Task<IActionResult> Privacy()
		{
			var userSessions = await _sessionManagementService.QuerySessionsAsync();
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
