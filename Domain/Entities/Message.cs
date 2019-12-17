using Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserMessage : Entity
    {
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }
}
