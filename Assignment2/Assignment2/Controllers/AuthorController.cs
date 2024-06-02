using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    public class AuthorController : ApiControllerBase
    {
        Assignment2Context _context = new Assignment2Context();

        [HttpGet]
        public ActionResult GetAuthors()
        {
            var publishers = _context.Authors.ToList();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public ActionResult GetAuthorById(int id)
        {
            var publisher = _context.Authors.FirstOrDefault(x => x.AuthorId == id);
            return Ok(publisher);
        }

        [HttpPost]
        public ActionResult AddAuthor(Author author)
        {
            var existAuthor = _context.Authors.FirstOrDefault(c =>
                c.LastName == author.LastName &&
                c.City == author.City &&
                c.State == author.State &&
                c.Address == author.Address &&
                c.FirstName == author.FirstName &&
                c.Phone == author.Phone &&
                c.Zip == author.Zip &&
                c.EmailAddress == author.EmailAddress);

            if (existAuthor == null)
            {
                _context.Authors.Add(author);
                _context.SaveChanges();
                return Ok(author);
            }
            else
            {
                return Ok("Author existed, try again");
            }
        }

        [HttpPut]
        public ActionResult UpdateAuthor(Author author)
        {
            var existAuthor = _context.Authors.FirstOrDefault(c => c.AuthorId == author.AuthorId);

            if (existAuthor != null)
            {
                existAuthor.LastName = author.LastName;
                existAuthor.FirstName = author.FirstName;
                existAuthor.City = author.City;
                existAuthor.Address = author.Address;
                existAuthor.State = author.State;
                existAuthor.Phone = author.Phone;
                existAuthor.Zip = author.Zip;
                existAuthor.EmailAddress = author.EmailAddress;

                _context.SaveChanges();
                return Ok(existAuthor);
            }
            else
            {
                return Ok("Author not exist, try again");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor(int id)
        {
            var existAuthor = _context.Authors.FirstOrDefault(x => x.AuthorId == id);

            if (existAuthor != null)
            {
                _context.Authors.Remove(existAuthor);
                _context.SaveChanges();
                return Ok("Delete Successfully");
            }
            else
            {
                return Ok("Author not exist, try again");
            }
        }
    }
}
