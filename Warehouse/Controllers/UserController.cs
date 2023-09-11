using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Xml.Linq;
using Warehouse.Models;
using Warehouse.Service.IService;
using Warehouse.Utility;

namespace Warehouse.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin,Value="0"},
                new SelectListItem{Text=SD.RoleStaff,Value="1"},
            };

            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            try
            {
                UserDto user = new UserDto
                {
                    ID = 0,
                    Email = login.Email,
                    Password = login.Password,
                    Name = string.Empty,
                    PhoneNumber = string.Empty,
                    Status = 0
                };
                ResponseDto result = await userService.LoginAsync(user);

                if (result != null && result.IsSuccess)
                {
                    user = JsonConvert.DeserializeObject<UserDto>(Convert.ToString(result.Result));
                    TempData["success"] = "Login Successful";
                    httpContextAccessor.HttpContext.Session.SetString("Name", user.Name);
                    httpContextAccessor.HttpContext.Session.SetString("ID", user.ID.ToString());
                    httpContextAccessor.HttpContext.Session.SetString("status", user.Status.ToString());

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = "Login Failed";
                }

            }
            catch(Exception ex)
            {
                TempData["error"] = ex;
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto user)
        {
            try
            {
                ResponseDto result = await userService.RegisterAsync(user);

                if (result != null && result.IsSuccess)
                {
                    TempData["success"] = "Registration Successful";
                    httpContextAccessor.HttpContext.Session.SetString("Name", user.Name);
                    httpContextAccessor.HttpContext.Session.SetString("ID", user.ID.ToString());
                    httpContextAccessor.HttpContext.Session.SetString("status", user.Status.ToString());

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex;
            }
            

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin,Value="0"},
                new SelectListItem{Text=SD.RoleStaff,Value="1"},
            };

            ViewBag.RoleList = roleList;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            httpContextAccessor.HttpContext.Session.SetString("Name", "");
            httpContextAccessor.HttpContext.Session.SetString("ID", "");
            httpContextAccessor.HttpContext.Session.SetString("status", "");
            return RedirectToAction("Index", "Home");
        }
    }
}
