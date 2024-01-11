using ApiCrudOperationsUsers.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrudOperationsUsers.Controllers
{
    public class UserController : Controller
    {
        private readonly Apigateway apigateway;
        public UserController(Apigateway apigateway)
        {
            this.apigateway = apigateway;
        }
        public IActionResult Index()
        {
            List<User> users;
            users = apigateway.ListUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            apigateway.CreateUser(user);
            return RedirectToAction("index");
        }

        public IActionResult Details(int id) { 
            User user = new User();
            user = apigateway.GetUser(id);
            return View(user);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            User user;
            user = apigateway.GetUser(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            apigateway.UpdateUser(user);
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            User user;
            user = apigateway.GetUser(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Delete(User user)
        {
            apigateway.DeleteUser(user.id);
            return RedirectToAction("index");
        }
    }
}
