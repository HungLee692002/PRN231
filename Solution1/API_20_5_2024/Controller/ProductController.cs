using API_20_5_2024.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_20_5_2024.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        MySaleDBContext _context = new MySaleDBContext();

        [HttpGet("GetList")]
        public ActionResult Get()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}/{name}")]
        public ActionResult GetProductByIdAndName (int id,string name)
        {
            var product = _context.Products.Where(x => x.ProductId == id && x.ProductName.Equals(name)).ToList();

            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            var existProduct = _context.Products.Where(x => x.ProductId == product.ProductId).FirstOrDefault();

            if(existProduct != null)
            {
                return Ok("Product existed, try again");
            } else
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Product product, int id)
        {
            var existProduct = _context.Products.Where(x => x.ProductId == product.ProductId).FirstOrDefault();


            if (existProduct == null)
            {
                return Ok("Product not existed, try again");
            }
            else
            {
                existProduct.ProductName = product.ProductName;
                existProduct.Image = product.Image;
                existProduct.CategoryId = product.CategoryId;
                existProduct.UnitPrice = product.UnitPrice;
                existProduct.UnitsInStock = product.UnitsInStock;
            }

            _context.SaveChanges();

            return Ok(existProduct);
        }

    }
}
