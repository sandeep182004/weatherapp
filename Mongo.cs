
using BlazorApp1.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlazorApp1.DBConnections
{
    public class Mongo
    {
        private readonly IMongoDatabase _database;

        public Mongo(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("Users");
            }
        }
    }
    public class Favorite
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string UserId { get; set; } // To associate favorites with a user
        public string LocationName { get; set; }
        public string LocationId { get; set; } // Optional: ID from the weather API
        public DateTime CreatedAt { get; set; }
    }
}