using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BaseComment : Entity
    {
        public string Text { get; set; }

        public DateTime Created { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string PostId { get; set; }

        [BsonIgnoreIfDefault]
        public string[] WhoLiked { get; set; } = new string[] { };

        [BsonIgnoreIfDefault]
        public string[] WhoDisliked { get; set; } = new string[] { };
    }
}
