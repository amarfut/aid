using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class ReactionDto
    {
        public ReactionDto(int likes, int dislikes, Reaction reaction)
        {
            Likes = likes;
            Dislikes = dislikes;
            Reaction = reaction;
        }

        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public Reaction Reaction { get; set; }
    }
}
