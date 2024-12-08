﻿using Demo.DataAcessLayer.Models;
using Demo.PresnationLayer.Helpers;
using Demo.PresnationLayer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PresnationLayer.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
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
					return RedirectToAction(nameof(Login));
				else
				{
					foreach (var error in Result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(model);
		}

		//Login

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);
				if (User is not null)
				{
					var Result = await _userManager.CheckPasswordAsync(User, model.Password);

					if (Result)
					{
						var LoginResult = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
						if (LoginResult.Succeeded)
							return RedirectToAction("Index", "Home");
					}
					else
						ModelState.AddModelError(string.Empty, "Password is incorrect");
				}
				else
					ModelState.AddModelError(string.Empty, "Email is Not Exist");
			}

			return View(model);
		}

		//Sign Out

		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}

		//Forget Password

		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);
				if (User is not null)
				{
					// valid for only one Time For This User
					var Token = await _userManager.GeneratePasswordResetTokenAsync(User);
					//https://localhost:44318/Account/ResetPassword?emai
					var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = User.Email, Token = Token }, Request.Scheme);
					// Send Email
					var email = new Email()
					{
						Subject = "Reset Password",
						To = User.Email,
						Body = ResetPasswordLink
					};

					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Email is not Exist!");
				}
			}
			return View("ForgetPassword", model);
		}

		public IActionResult CheckYourInbox()
		{
			return View();
		}

		//Reset Password

		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				string email = TempData["email"] as string;
				string token = TempData["token"] as string;
				var user = await _userManager.FindByEmailAsync(email);
				var Result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
				if (Result.Succeeded)
				{
					return RedirectToAction(nameof(Login));
				}

				else
				{
					foreach (var error in Result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}

				//await _userManager.ResetPasswordAsync();
			}
			return View(model);
		}
	}
}