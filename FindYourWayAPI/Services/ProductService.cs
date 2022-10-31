using FindYourWayAPI.Data;
using FindYourWayAPI.Models;
using FindYourWayAPI.Models.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindYourWayAPI.Services
{
    public class ProductService
    {
        private readonly FindYourWayDbContext _context;
        private readonly CompanyService companyService;

        public ProductService(FindYourWayDbContext context, CompanyService companyService)
        {
            _context = context;
            this.companyService = companyService;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var list = await _context.Products
               .ToListAsync();
            return list;
        }
        public async Task<IEnumerable<Product>> GetCompanyProducts(int id)
        {
            if (!companyService.CompanyExists(id)) return null;

            return await _context.Products.Where(p => p.CompanyId == id).ToListAsync(); 
        }
        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
        public async Task<Product> AddProduct(AddProductRequest product)
        {
            var company = await companyService.GetCompany(product.CompanyId);
            var newProduct = new Product
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                CompanyId = company.CompanyId,
                Company = company
            };
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return newProduct;
        }
        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

        }
        public async Task PutProduct(int id, Product product)
        {
            
            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();
           
        }
    }
}
