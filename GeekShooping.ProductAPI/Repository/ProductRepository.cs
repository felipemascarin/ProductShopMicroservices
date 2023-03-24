using AutoMapper;
using GeekShooping.ProductAPI.Data.ValueObjects;
using GeekShooping.ProductAPI.Model;
using GeekShooping.ProductAPI.Model.Context;
using GeekShooping.ProductAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GeekShooping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> Post(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetById(long productId)
        {
            var product = await _context.Products.Where(p => p.Id == productId).FirstOrDefaultAsync() ?? new Product();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> Put(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return productDto;
        }

        public async Task<bool> Delete(long productId)
        {
            try
            {
                var product = await _context.Products.Where(p => p.Id == productId).FirstOrDefaultAsync() ?? new Product();
                if (product == null) return false;
                else _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}