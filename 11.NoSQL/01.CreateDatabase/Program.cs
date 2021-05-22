using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace _01.CreateDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("NoSQL");
            var collection = database.GetCollection<BsonDocument>("Articles");

            BsonArray articles = BsonSerializer.Deserialize<BsonArray>(File.ReadAllText(@"Datasets\articles.json"));
            foreach (var article in articles)
            {
                collection.InsertOne(article.AsBsonDocument);
            }
        }
    }
}
