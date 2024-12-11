using CRUD_Productos.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Productos.Server.Controllers
{
    [ApiController, Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly SQLDBContext _context;

        public ProductController(SQLDBContext context)
        {
            _context = context;
        }

        [HttpGet, Route("getproducts")]
        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                var list = await _context.Product.ToListAsync();
                return list;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        [HttpGet, Route("getproduct/{id}")]
        public async Task<Product> GetProductByIdAsync(int Id)
        {
            try
            {
                var product = await _context.Product
                    .FindAsync(Id);
                return product!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        [HttpPost, Route("addproduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                if (product != null)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (Exception)
            {
                return null!;
            }

            return NoContent();
        }

        [HttpPost, Route("editproduct")]
        public async Task<IActionResult> EditProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(product.Id))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        private bool Exists(int id)
        {
            return (_context.Product?.Any(p => p.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("deleteproduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id) 
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else 
            {
                return NotFound();
            }
        }
    }
}
