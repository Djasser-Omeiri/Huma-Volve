using MediatR;
using Microsoft.AspNetCore.Mvc;
using OmeiriDjasser.Application.Products.Commands;
using OmeiriDjasser.Application.Products.Queries;
using OmeiriDjasser.Application.Products.DTOs;
using Serilog;

namespace OmeiriDjasser.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // POST /api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDTO dto)
        {
            _logger.LogInformation("Creating new product with name: {ProductName} and price: {ProductPrice}", dto.Name, dto.Price);
            try
            {
                var command = new CreateProductCommand(dto.Name, dto.Price);
                var id = await _mediator.Send(command);
                _logger.LogInformation("Product created successfully with ID: {ProductId}", id);
                return CreatedAtAction(nameof(GetById), new { id }, new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product with name: {ProductName}", dto.Name);
                throw;
            }
        }

        // GET /api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching product with ID: {ProductId}", id);
            try
            {
                var product = await _mediator.Send(new GetProductByIdQuery(id));
                if (product == null)
                {
                    _logger.LogWarning("Product not found with ID: {ProductId}", id);
                    return NotFound();
                }
                _logger.LogInformation("Product retrieved successfully with ID: {ProductId}", id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching product with ID: {ProductId}", id);
                throw;
            }
        }

        // GET /api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all products");
            try
            {
                var products = await _mediator.Send(new GetProductsQuery());
                _logger.LogInformation("Retrieved {ProductCount} products", products.Count());
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all products");
                throw;
            }
        }
    }
}
