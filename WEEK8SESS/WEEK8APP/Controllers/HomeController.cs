using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WEEK8APP.DAO;
using WEEK8APP.DAO.Interface;
using WEEK8APP.DTO;
using WEEK8APP.DTO.ViewModels;
using WEEK8APP.Models;

namespace WEEK8APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IUSER userRepo;

        private INidaGenerator generator;

        private int a = 0;
        public HomeController(ILogger<HomeController> logger, IUSER _user, INidaGenerator gen)
        {
            _logger = logger;
            userRepo = _user;
            generator = gen;
        }

        public IActionResult Index()
        {
            var users = userRepo.GetAllUsers();

            return View(users);
        }

        [HttpGet]
        public IActionResult RegisterUser(User user)
        {

            return View(user);
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                userRepo.CreateUser(user);

                return RedirectToAction("Index", "Home", new { message = "{0}", user });
            }
            else
            {
                return RedirectToAction("Index", "Home", new { message = "Couldnot create a New User!." });
            }
        }

        [HttpGet]
        public IActionResult SetNidaNumber(long id)
        {
            var user = userRepo.GetCurrentUser(id);

            user.NidaNumber = generator.Generate();
            a = 1;
            userRepo.UpdateUser(user);

            


            return RedirectToAction("Index", "Home");
        }


            private string MessageTempelate(string fullName, string nidaCloneNo)
        {
            var messageBody = string.Format("Hello Mr. {0}, your NidaClone Identification number {1}, NB: Dont use it in real-life."
                , fullName, nidaCloneNo);

            return messageBody;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
