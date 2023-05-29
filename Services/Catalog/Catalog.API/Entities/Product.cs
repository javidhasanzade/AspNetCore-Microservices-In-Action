using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("category")]
    public string Category { get; set; }
    
    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("seller")]
    public string Seller { get; set; }

    [BsonElement("quantity")]
    public int Quantity { get; set; }
    
    [BsonElement("image_file")]
    public string ImageFile { get; set; }
    
    [BsonElement("price")]
    public decimal Price { get; set; }
}