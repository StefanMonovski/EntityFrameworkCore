using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace _02.ReadData
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
                Console.WriteLine(article.GetElement("name").Value);
            }
        }
    }
}