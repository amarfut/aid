
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{

    public class SimilarPost
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }

    

    public class Post : Entity
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public int Type { get; set; }

        public DateTime Created { get; set; }

        public int CommentsCount { get; set; }
        
        public int Views { get; set; }

        public string Description { get; set; }

        [BsonIgnoreIfDefault]
        public string[] WhoLiked { get; set; } = new string[] { };

        [BsonIgnoreIfDefault]
        public string[] WhoDisliked { get; set; } = new string[] { };

        [BsonIgnoreIfDefault]
        public SimilarPost[] Similar { get; set; } = new SimilarPost[] { };

        [BsonIgnoreIfDefault]
        public string[] Tags { get; set; } = new string[] { };
    }
}
