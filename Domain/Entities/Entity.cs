using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public abstract class Entity
    {
        [BsonId]
        public ObjectId InternalId { get; set; }

        [BsonIgnore]
        public string Id => InternalId.ToString();
    }
}
