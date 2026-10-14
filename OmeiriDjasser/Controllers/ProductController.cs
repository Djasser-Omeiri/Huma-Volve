using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmeiriDjasser.Data;
using OmeiriDjasser.DTOs;
namespace OmeiriDjasser.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResultDTO<ProductResponseDTO>>> GetProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be positive.");
            }

            pageSize = Math.Min(pageSize, 100);

            var query = context.Products
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductResponseDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category!.Name,
                    RowVersion = p.RowVersion
                })
                .ToListAsync();

            var result = new PagedResultDTO<ProductResponseDTO>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(
                    totalCount / (double)pageSize)
            };

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            product.IsDeleted = true;

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            [FromBody] UpdateProductDTO request)
        {
            if (request.RowVersion is null ||
                request.RowVersion.Length == 0)
            {
                return BadRequest("RowVersion is required.");
            }

            var product = await context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
                return NotFound();

            var categoryExists = await context.Categories
                .AnyAsync(c => c.Id == request.CategoryId);

            if (!categoryExists)
                return BadRequest("The specified category does not exist.");

            context.Entry(product)
                .Property(p => p.RowVersion)
                .OriginalValue = request.RowVersion;

            product.Name = request.Name;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(
                    "The product was modified by another request. Reload it and try again.");
            }

            return NoContent();
        }
    }
}
