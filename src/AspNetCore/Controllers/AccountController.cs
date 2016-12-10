using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AspNetCore.Models.ViewModels;
using AspNetCore.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IOrderRepository repository;
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        int pageSize = 5;

        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr,
            IOrderRepository repoService)
        {
            repository = repoService;
            userManager = userMgr;
            signInManager = signInMgr;
        }
        // GET: /<controller>/
        public IActionResult Index(int page = 1)
        {
            return View(repository.GetOrderDetail(pageSize, page));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                    loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Admin/Index");
                    }
                }
            }
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            TempData["Error"] = "Tên đăng nhập hoặc mật khẩu không đúng";
            return Redirect(loginModel?.ReturnUrl ?? "/");
        }
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            if(returnUrl.Contains("Account"))
            {
                return Redirect("/");
            }
            return Redirect(returnUrl);
        }
    }
}
