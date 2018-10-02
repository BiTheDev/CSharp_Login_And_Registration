using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginRegister.Models;
using Microsoft.AspNetCore.Identity;

namespace LoginRegister.Controllers
{
    public class HomeController : Controller
    {

        private LoginContext _context;
        public HomeController(LoginContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("index");
        }

        [HttpPost("/register")]
        public IActionResult Register(RegisterViewModel user){
            if(ModelState.IsValid){
                PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                user.password = Hasher.HashPassword(user, user.password);
                LoginAndRegister User = new LoginAndRegister(){
                    first_name = user.first_name,
                    last_name = user.last_name,
                    email = user.email,
                    password = user.password,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
                _context.Add(User);
                _context.SaveChanges();
                return RedirectToAction("Success");
            }else{
                return View("index");
            }
        }

        [HttpPost("/login")]
        public IActionResult Login(string Email, string Password){
                if (Email == null || Password == null){
                        ViewBag.error = "Invalid email or password";
                         return View("index");
                }else{
                List<LoginAndRegister> users = _context.LoginAndRegister.Where(p => p.email== Email).ToList();
                    foreach (var user in users)
                    {
                        if(user != null && Password != null){
                                var Hasher = new PasswordHasher<LoginAndRegister>();
                                if( 0 !=Hasher.VerifyHashedPassword(user, user.password, Password)){
                                return RedirectToAction("Success");
                                }else if(Password != user.password){
                                    ViewBag.error = "Invalid email or password";
                                return View("index");
                            }
                        }
                            
                    }
                    ViewBag.error = "Invalid email or password";
                    return View("Index");
                }     
        }

        [HttpGet("/success")]
        public IActionResult Success(){
            return View("result");
        }

     
    }
}
