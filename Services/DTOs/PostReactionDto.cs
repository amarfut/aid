using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class PostReactionDto
    {
        public string PostId { get; set; }

        public bool Liked { get; set; }
    }
}
