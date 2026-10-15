using OmeiriDjasser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmeiriDjasser.Application.Common.Interfaces
{
    public interface IProductRepository
    {
        Task<int> AddAsync(Product product);
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
    }
}
