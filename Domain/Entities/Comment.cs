using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Comment : BaseComment
    {
        [BsonIgnoreIfDefault]
        public bool IsDeleted { get; set; }

        [BsonIgnoreIfDefault]
        public Answer[] Answers { get; set; } = new Answer[0];
    }
}
