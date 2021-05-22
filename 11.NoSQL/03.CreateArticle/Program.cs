using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace _03.CreateArticle
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("NoSQL");
            var collection = database.GetCollection<BsonDocument>("Articles");

            BsonDocument article = new BsonDocument()
            {
                { "author", "Steve Jobs"},
                { "date", "05-05-2005" },
                { "name", "The story of Apple" },
                { "rating", "50" }
            };

            collection.InsertOne(article);
        }
    }
}
