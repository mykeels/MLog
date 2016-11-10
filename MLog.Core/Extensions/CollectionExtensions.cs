using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MLog.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static DeleteResult Clear<T>(this IMongoCollection<T> collection)
        {
            return collection.DeleteMany(FilterDefinition<T>.Empty);
        }
    }
}
