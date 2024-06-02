using Assignment2.Models;
using Assignment2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment2.Pages
{
    public class RegisterModel : PageModel
    {
        public UserService Service = new UserService();

        public string email { get; set; }

        public string password { get; set; }
        public string passwordConfirm { get; set; }

        public string Error { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            email = Request.Form["txtEmail"];
            password = Request.Form["txtPass"];
            passwordConfirm = Request.Form["txtPassConfirm"];

            if (password.Equals(passwordConfirm))
            {
                User user = new User
                {
                    EmailAddress = email,
                    Password = password
                };

                RequestResponse<User> res = await Service.UserRegister(user);

                if (res.Success)
                {
                    return RedirectToPage("/Login");
                }
                else
                {
                    Error = res.Message;
                }
            } else
            {
                Error = "Password not match! Please try again";
                
            }
            return Page();
        }
    }
}
