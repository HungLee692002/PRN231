using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    public class BookController : ApiControllerBase
    {
        Assignment2Context _context = new Assignment2Context();

        [HttpGet]
        public ActionResult GetBooks()
        {
            var publishers = _context.Books.ToList();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public ActionResult GetBookById(int id)
        {
            var publisher = _context.Books.FirstOrDefault(x => x.BookId == id);
            return Ok(publisher);
        }

        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            var existPublisher = _context.Publishers.FirstOrDefault(x => x.PubId == book.PubId);

            if(existPublisher != null) {
                var existBook = _context.Books.FirstOrDefault(c =>
                    c.Title == book.Title &&
                    c.Type == book.Type &&
                    c.PubId == book.PubId &&
                    c.Price == book.Price &&
                    c.Advance == book.Advance &&
                    c.Royalty == book.Royalty &&
                    c.YtdSales == book.YtdSales &&
                    c.PublishedDate == book.PublishedDate &&
                    c.Notes == book.Notes);

                if (existBook == null)
                {
                    _context.Books.Add(book);
                    _context.SaveChanges();
                    return Ok(book);
                }
                else
                {
                    return Ok("Book existed, try again");
                }
            } else
            {
                return NotFound("Publisher not exist");
            }
        }

        [HttpPut]
        public ActionResult UpdateBook(Book book)
        {
            var existBook = _context.Books.FirstOrDefault(c => c.PubId == book.PubId);

            if (existBook != null)
            {
                var existPublisher = _context.Publishers.FirstOrDefault(x => x.PubId == book.PubId);

                if (existPublisher != null)
                {
                    existBook.Title = book.Title;
                    existBook.Type = book.Type;
                    existBook.PubId = book.PubId;
                    existBook.Price = book.Price;
                    existBook.Advance = book.Advance;
                    existBook.Royalty = book.Royalty;
                    existBook.YtdSales = book.YtdSales;
                    existBook.Notes = book.Notes;
                    existBook.PublishedDate = book.PublishedDate;

                    _context.SaveChanges();
                    return Ok(book);
                } else
                {
                    return NotFound("Publisher not exist");
                }
            }
            else
            {
                return Ok("Book not exist, try again");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var publisher = _context.Books.FirstOrDefault(x => x.BookId == id);

            if (publisher != null)
            {
                _context.Books.Remove(publisher);
                _context.SaveChanges();
                return Ok("Delete Successfully");
            }
            else
            {
                return Ok("Book not exist, try again");
            }
        }
    }
}
