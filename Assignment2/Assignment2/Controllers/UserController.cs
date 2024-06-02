using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    public class UserController : ApiControllerBase
    {
        Assignment2Context _context = new Assignment2Context();

        [HttpPost]
        public ActionResult GetUserLogin(User user)
        {
            var u = _context.Users.FirstOrDefault(x =>
                x.EmailAddress.Equals(user.EmailAddress) &&
                x.Password.Equals(user.Password));

            if (u == null)
            {
                return NotFound("User Not Exist");
            }

            return Ok(u);
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            var existUser = _context.Users.FirstOrDefault(c => c.EmailAddress.Equals(user.EmailAddress));

            if (existUser == null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok(user);
            }
            else
            {
                return BadRequest("Email already in use");
            }
        }
    }
}
