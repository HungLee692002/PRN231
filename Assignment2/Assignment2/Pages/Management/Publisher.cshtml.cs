using Assignment2.Models;
using Assignment2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Pages.Management
{
    public class PublisherModel : PageModel
    {
        public PublisherService Service = new PublisherService();

        private readonly Assignment2Context _context = new Assignment2Context();

        public List<Publisher> Publishers = new List<Publisher>();

        public List<string> CityList = new List<string>();

        public string publisherName { get; set; }
        public string city { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? PubId { get; set; }

        public string Error { get; set; }

        public async Task OnGetAsync()
        {
            if (PubId.HasValue)
            {
                RequestResponse<Publisher> resp = await Service.GetPublisherById(PubId.Value);

                publisherName = resp.item.PublisherName;
                city = resp.item.City;
            }

            RequestResponse<Publisher> res = await Service.GetPublishers();

            Publishers = res.items.ToList();

            GetCity();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            RequestResponse<Publisher>? res = null;

            if (!PubId.HasValue)
            {
                Publisher author = new Publisher
                {
                    PublisherName = Request.Form["txtName"],
                    City = Request.Form["ddlCity"],
                };

                res = await Service.AddPublisher(author);
            }
            else
            {
                res = await Service.GetPublisherById(PubId.Value);

                res.item.PublisherName = Request.Form["txtFirstName"];
                res.item.City = Request.Form["ddlCity"];

                res = await Service.UpdatePublisher(res.item);
            }

            if (!res.Success)
            {
                Error = res.Message;
            }

            return RedirectToPage("/Management/Publisher");
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            RequestResponse<Publisher> res = await Service.DeletePublisher(id);

            if (!res.Success)
            {
                Error = res.Message;
            }

            return RedirectToPage("/Management/Publisher");
        }

        public async Task GetCity()
        {
            var City = await this._context.Publishers.Select(x => x.City).Distinct().ToListAsync();

            CityList = City;
        }
    }
}
