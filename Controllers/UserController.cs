using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sud.Models;

namespace sud.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        ApplicationContext dataBase;
        public UserController(ApplicationContext context)
        {
            dataBase = context;
            if (!dataBase.Users.Any())
            {
                dataBase.Users.Add(new User { UserName = "Vladimir", UserLastName = "Petrov", Age = 11 });
                dataBase.Users.Add(new User { UserName = "Ivan", UserLastName = "Glukhov", Age = 28 });
                dataBase.Users.Add(new User { UserName = "Dmitry", UserLastName = "Obama", Age = 99 });
                dataBase.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return dataBase.Users.ToList();
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            User userById = dataBase.Users.FirstOrDefault(user => user.Id == id);
            return userById;
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            if (ModelState.IsValid)
            {
                dataBase.Users.Add(user);
                dataBase.SaveChanges();
                return Ok(user);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put(User user)
        {
            if (ModelState.IsValid)
            {
                dataBase.Update(user);
                dataBase.SaveChanges();
                return Ok(user);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User user = dataBase.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                dataBase.Users.Remove(user);
                dataBase.SaveChanges();
            }
            return Ok(user);
        }
    }
}
