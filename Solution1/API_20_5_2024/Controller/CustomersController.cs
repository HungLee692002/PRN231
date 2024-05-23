using API_20_5_2024.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_20_5_2024.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        MySaleDBContext _context = new MySaleDBContext();

        [HttpGet]
        public ActionResult Get()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        [HttpPost]
        public ActionResult Post(Customer customer)
        {
            var existedCustomer = GetExistedCustomer(customer);

            if (existedCustomer != null)
            {
                return Ok("User existed, try again");
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Customer customer, int id) {
            var existingCustomer = GetCustomerById(id);


            if(existingCustomer == null)
            {
                return NotFound("User not existed, try again");
            }

            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.Gender = customer.Gender;
            existingCustomer.Address = customer.Address;
            existingCustomer.Birthdate = customer.Birthdate;

            _context.SaveChanges();

            return Ok(existingCustomer);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingCustomer = GetCustomerById(id);

            if (existingCustomer == null)
            {
                return NotFound("Customer not existed, try again");
            }

            _context.Customers.Remove(existingCustomer);
            _context.SaveChanges();

            return Ok("Delete successfully");

        }

        private Customer GetCustomerById (int id)
        {
            var customer = _context.Customers.FirstOrDefault(
                c => c.CustomerId == id);

            return customer;
        }

        private Customer GetExistedCustomer(Customer customer)
        {
            var existedCustomer = _context.Customers.FirstOrDefault(c =>
               c.CustomerName == customer.CustomerName &&
               c.Gender == customer.Gender &&
               c.Address == customer.Address &&
               c.Birthdate == customer.Birthdate
               );
            return existedCustomer;
        }

        [HttpGet("GetList")]
        public ActionResult GetList()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        [HttpGet("{id}/{name}")]
        public ActionResult GetCustomerByIdAndName(int id, string name)
        {
            var product = _context.Customers.Where(x => x.CustomerId == id && x.CustomerName.Equals(name)).ToList();

            return Ok(product);
        }
    }
}
