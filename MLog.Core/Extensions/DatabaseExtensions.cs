using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MLog.Core.Extensions
{
    public static class DatabaseExtensions
    {
        public static bool CollectionExists(this IMongoDatabase db, string collectionName)
        {
            return db.ListCollections(new ListCollectionsOptions()
            {
                Filter = new BsonDocument("name", collectionName)
            }).Any();
        }

        public static IMongoCollection<BsonDocument> CreateNewCollection(this IMongoDatabase db, string collectionName)
        {
            if (!db.CollectionExists(collectionName))
            {
                db.CreateCollection(collectionName);
            }
            return db.GetCollection<BsonDocument>(collectionName);
        }

        public static IMongoCollection<TDocument> CreateNewCollection<TDocument>(this IMongoDatabase db, string collectionName, MongoCollectionSettings settings = null)
        {
            db.CreateNewCollection(collectionName);
            return db.GetCollection<TDocument>(collectionName, settings);
        }
    }
}
