using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Controllers
{
    public class PublisherController : ApiControllerBase
    {
        Assignment2Context _context = new Assignment2Context();

        [HttpGet]
        public ActionResult GetPublishers()
        {
            var publishers = _context.Publishers.ToList();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public ActionResult GetPublisherById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(x => x.PubId == id);
            return Ok(publisher);
        }

        [HttpPost]
        public ActionResult AddPublisher(Publisher publisher)
        {
            var existPublisher = _context.Publishers.FirstOrDefault(c => 
                c.PublisherName == publisher.PublisherName && 
                c.City == publisher.City && 
                c.State == publisher.State && 
                c.Country == publisher.Country);

            if (existPublisher == null)
            {
                _context.Publishers.Add(publisher);
                _context.SaveChanges();
                return Ok(publisher);
            }
            else
            {
                return Ok("Publisher existed, try again");
            }
        }

        [HttpPut]
        public ActionResult UpdatePublisher(Publisher publisher)
        {
            var existPublisher = _context.Publishers.FirstOrDefault(c =>c.PubId == publisher.PubId);

            if (existPublisher != null)
            {
                existPublisher.PublisherName = publisher.PublisherName;
                existPublisher.City = publisher.City;
                existPublisher.Country = publisher.Country;
                existPublisher.State = publisher.State;

                _context.SaveChanges();
                return Ok(publisher);
            }
            else
            {
                return Ok("Publisher not exist, try again");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePublisher(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(x => x.PubId == id);

            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
                return Ok("Delete Successfully");
            }
            else
            {
                return Ok("Publisher not exist, try again");
            }
        }

    }
}
