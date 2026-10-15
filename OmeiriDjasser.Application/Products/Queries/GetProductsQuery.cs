using MediatR;
using OmeiriDjasser.Application.Products.DTOs;

namespace OmeiriDjasser.Application.Products.Queries
{
    public record GetProductsQuery() : IRequest<List<ProductDTO>>;
}
