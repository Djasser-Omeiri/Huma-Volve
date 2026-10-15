using MediatR;
using OmeiriDjasser.Application.Products.DTOs;
using OmeiriDjasser.Application.Common.Interfaces;

namespace OmeiriDjasser.Application.Products.Queries
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDTO?>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDTO?> Handle(GetProductByIdQuery request, CancellationToken ct)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null) return null;

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
