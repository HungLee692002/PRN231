using Assignment2.Models;
using Assignment2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Pages.Management
{
    public class AuthorsModel : PageModel
    {
        private readonly Assignment2Context _context = new Assignment2Context();

        public AuthorService Service = new AuthorService();

        public List<Author> Authors = new List<Author>();

        public List<string> CityList = new List<string>();

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string city { get; set; }
        public string email { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? AuthorId { get; set; }

        public string Error { get;set; }

        public async Task OnGetAsync()
        {
            if (AuthorId.HasValue)
            {
                RequestResponse<Author> resp = await Service.GetAuthorById(AuthorId.Value);

                firstName = resp.item.FirstName;
                lastName = resp.item.LastName;
                city = resp.item.City;
                email = resp.item.EmailAddress;
            }

            RequestResponse<Author> res = await Service.GetAuthor();

            Authors = res.items.ToList();

            GetCity();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            RequestResponse<Author>? res = null;

            if (!AuthorId.HasValue)
            {
                Author author = new Author
                {
                    FirstName = Request.Form["txtFirstName"],
                    LastName = Request.Form["txtLastName"],
                    City = Request.Form["ddlCity"],
                    EmailAddress = Request.Form["txtEmail"],
                };

                res = await Service.AddAuthor(author);
            }
            else
            {
                res = await Service.GetAuthorById(AuthorId.Value);

                res.item.FirstName = Request.Form["txtFirstName"];
                res.item.LastName = Request.Form["txtLastName"];
                res.item.City = Request.Form["ddlCity"];
                res.item.EmailAddress = Request.Form["txtEmail"];

                res = await Service.UpdateAuthor(res.item);
            }

            if (!res.Success)
            {
                Error = res.Message;
            }

            return RedirectToPage("/Management/Authors");
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            RequestResponse<Author> res = await Service.DeleteAuthor(id);

            if (!res.Success)
            {
                Error = res.Message;
            }

            return RedirectToPage("/Management/Authors");
        }

        public async Task GetCity()
        {
            var City = await this._context.Authors.Select(x => x.City).Distinct().ToListAsync();

            CityList = City;
        }

    }
}
