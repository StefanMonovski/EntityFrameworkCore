using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace _04.UpdateData
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("NoSQL");
            var collection = database.GetCollection<BsonDocument>("Articles");

            List<BsonDocument> articles = collection.Find(new BsonDocument()).ToList();
            foreach (var article in articles)
            {
                var update = Builders<BsonDocument>.Update.Set("rating", (int.Parse(article.GetElement("rating").Value.AsString) + 10).ToString());
                collection.UpdateOne(article, update);
            }
        }
    }
}
