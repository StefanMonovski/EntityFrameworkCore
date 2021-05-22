using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace _05.DeleteData
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("NoSQL");
            var collection = database.GetCollection<BsonDocument>("Articles");

            List<BsonDocument> articlesBeforeDelete = collection.Find(new BsonDocument()).ToList();
            foreach (var article in articlesBeforeDelete)
            {
                if (int.Parse(article.GetElement("rating").Value.AsString) <= 50)
                {
                    collection.DeleteOne(article);
                }
            }
            
            List<BsonDocument> articlesAfterDelete = collection.Find(new BsonDocument()).ToList();
            foreach (var article in articlesAfterDelete)
            {
                Console.WriteLine(article.GetElement("name").Value);
            }
        }
    }
}