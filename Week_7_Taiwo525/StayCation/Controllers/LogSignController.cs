using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StayCation.Models;
using StayCation.Repository;

namespace StayCation.Controllers
{
    public class LogSignController : Controller
    {
        private readonly IRepository _repository;
        public LogSignController(IRepository repository)
        {
           _repository = repository;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Customers cust)
        {
            var alluser = _repository.ReadCustomersFromFile("StayRegFile.txt");
            string e = cust.Email;
            string p = cust.PassWord;

            var valid = alluser.FirstOrDefault(user => user.Email == e && user.PassWord == p);

            if(valid != null)
            {
                return RedirectToAction("Sign_Up");
            }
            else
            {
                Console.WriteLine("Failed to log in");
            }
            return View();
        }

        public IActionResult Sign_Up()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sign_Up(Customers dto)
        {
            
            if (ModelState.IsValid)
            {
                using (StreamWriter sw = new StreamWriter("StayRegFile.txt", true))
                {
                    sw.WriteLine($"| {dto.Id} | {dto.Name} | {dto.Email} | {dto.PassWord} | {dto.RegisteredOn}");
                }
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
