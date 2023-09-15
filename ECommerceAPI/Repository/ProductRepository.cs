using ECommerceAPI.Models;
using ECommerceAPI.Models.Data;
using MongoDB.Driver;

namespace ECommerceAPI.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly ICatelogContext _context;

        public ProductRepository(ICatelogContext context)
        {
            _context = context ?? throw new NotImplementedException(nameof(context)) ;
        }
       public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        } 
        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.Id==id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.ElemMatch(p => p.Name , name);
            return await _context.Products.Find(filterDefinition).ToListAsync(); 
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await _context.Products.Find(filterDefinition).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async  Task<bool> UpdateProduct(Product product)
        {
            var  updateResult = await _context.Products.ReplaceOneAsync(filter:p=>p.Id==product.Id , replacement:product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0; 
        }

        public async Task<bool> DeleteProduct(string id)
        {
            try
            {

            await _context.Products.DeleteOneAsync(p => p.Id == id);
                return true;
            }
            catch (Exception ex)
            {

        return false;
            }
            
        }


    }
}
