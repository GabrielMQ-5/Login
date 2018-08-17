using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Entities;
using Login.Infraestructure;
using Login.Models;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService userService;
        public HomeController()
        {
            this.userService = new UserService();
        }
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User objUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var output = userService.Login(objUser.Username, objUser.Password);
                    User user = output.Item1;
                    int result = output.Item2;
                    if (user == null)
                    {
                        switch (result)
                        {
                            case 1:
                                TempData["Message"] = "Error: Missing values";
                                break;                         
                            case 2:
                                TempData["Message"] = "Error: Username doesn't exist";
                                break;
                            case 3:
                                TempData["Message"] = "Error: Incorrect username/password combination";
                                break;
                            default:
                                TempData["Message"] = "Error: Incorrect field values";
                                break;
                        }
                        return RedirectToAction("Login", "Home");
                    }
                    Session["objUser"] = user;
                    return RedirectToAction("Dashboard", "Home");
                }
                TempData["Message"] = "Error: Incorrect field values";
                return View();
            }
            catch (Exception e)
            {
                TempData["Message"] = "Error: Connection to database failed";
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        public ActionResult Dashboard()
        {
            User user = (User)Session["objUser"];
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.user = user;
            dashboardViewModel.userFullname = user.Name + " " + user.Surname;
            return View(dashboardViewModel);
        }
    }
}