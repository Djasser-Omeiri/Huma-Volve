using MediatR;
using OmeiriDjasser.Application.Products.DTOs;
namespace OmeiriDjasser.Application.Products.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDTO?>;
}
