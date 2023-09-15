using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string? Name { get; set; }
        [BsonElement("Discription")]
        public string? Description { get; set; }
        public string? Summary{ get; set; }
        public string? ImageFile{ get; set; }
        public string? Price{ get; set; }
        public string? Category{ get; set; }


    }
}
