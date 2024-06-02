using Assignment2.Models;
using Assignment2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment2.Pages
{
    public class LoginModel : PageModel
    {
        public UserService Service = new UserService();

        public string email { get; set; }

        public string password { get; set; }

        public string Error { get; set; } 

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            email = Request.Form["txtEmail"];
            password = Request.Form["txtPass"];

            User user = new User
            {
                EmailAddress = email,
                Password = password
            };

            RequestResponse<User> res = await Service.UserLogin(user);

            if(res.Success)
            {
                return RedirectToPage("/Management/Index");
            } else
            {
                Error = res.Message;
            }

            return Page();
        }
    }
}
