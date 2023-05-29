using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _catalogContext;

    public ProductRepository(ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }
    
    public async Task<IEnumerable<Product>> GetProducts(int page=0)
    {
        return await _catalogContext.Products.AsQueryable()
            .Skip(9*page)
            .Take(9)
            .ToListAsync();
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _catalogContext.Products
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, name);

        return await _catalogContext.Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Category, category);

        return await _catalogContext.Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task CreateProduct(Product product)
    {
        await _catalogContext.Products.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await _catalogContext.Products
            .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
        
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        DeleteResult deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
}