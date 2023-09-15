using MongoDB.Driver;

namespace ECommerceAPI.Models.Data
{
    public class CatelogContext : ICatelogContext
    {
        public CatelogContext(IConfiguration configuration) 
        { 
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSetting:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSetting:DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSetting:CollectionName"));
             CatelogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
