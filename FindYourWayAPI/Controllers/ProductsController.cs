using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindYourWayAPI.Data;
using FindYourWayAPI.Models;
using FindYourWayAPI.Services;
using FindYourWayAPI.Models.DAO;

namespace FindYourWayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly FindYourWayDbContext _context;
        private readonly ProductService productService;
        private readonly CompanyService _companyService;

        public ProductsController(FindYourWayDbContext context , ProductService productService)
        {
            _context = context;
            this.productService = productService;
            _companyService = new CompanyService(_context);
        }

        // GET: api/Products
        /// <summary>
        /// Returns all products of all companies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var list = await productService.GetAllProducts();
            return Ok(list);
        }

        // GET: api/Products/5
        /// <summary>
        /// Returns all products of specified company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetCompanyProducts(int id)
        {
            var list = await productService.GetCompanyProducts(id);

            return Ok(list);
        }

        // GET: api/Products/Product/5
        /// <summary>
        /// Returns all products of specified category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("category/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllCategoryProducts(int id)
        {

            var list= await productService.GetAllCategoryProducts(id);
            return Ok(list);
        }


        // GET: api/Products/Product/5
        /// <summary>
        /// Returns all products of specified company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("product/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (!productService.ProductExists(id)) return BadRequest();

            return await productService.GetProduct(id);
        }
        // POST: api/Products
        /// <summary>
        /// Add a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(AddProductRequest product)
        {
            var newProduct = await productService.AddProduct(product);  
            return CreatedAtAction("GetProduct", new { id = newProduct.ProductId }, newProduct);
        }
        /*
        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        */
        // DELETE: api/Products/5
        /// <summary>
        /// Delete a product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if(!productService.ProductExists(id)) return NotFound();
            await productService.DeleteProduct(id);
            return NoContent();
        }
        
        
    }
}
