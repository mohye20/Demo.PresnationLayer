﻿using Demo.DataAcessLayer.Models;
using Demo.PresnationLayer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PresnationLayer.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		//Register
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = new ApplicationUser()
				{
					UserName = model.Email.Split('@')[0],
					Email = model.Email,
					IsAgree = model.IsAgree,
					FName = model.FName,
					LName = model.LName,
				};

				var Result = await _userManager.CreateAsync(User, model.Password);
				if (Result.Succeeded)
					return RedirectToAction("Login");
				else
				{
					foreach (var error in Result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(model);
		}

		//Login

		//Sign Out

		//Forget Password

		//Reset Password
	}
}