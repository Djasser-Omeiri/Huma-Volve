using MediatR;
using OmeiriDjasser.Application.Products.DTOs;
using OmeiriDjasser.Application.Common.Interfaces;

namespace OmeiriDjasser.Application.Products.Queries
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<ProductDTO>>
    {
        private readonly IProductRepository _repository;

        public GetProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDTO>> Handle(GetProductsQuery request, CancellationToken ct)
        {
            var products = await _repository.GetAllAsync();

            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }
    }
}
