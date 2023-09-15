using MongoDB.Driver;

namespace ECommerceAPI.Models.Data
{
    public interface ICatelogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
