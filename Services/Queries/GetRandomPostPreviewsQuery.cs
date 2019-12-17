using Domain;
using Domain.Entities;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Queries
{
    public class GetRandomPostPreviewsQuery : IQuery<List<PostPreviewDto>>
    {
        public GetRandomPostPreviewsQuery(PostType type, int number)
        {
            PostType = type;
            Number = number;
        }

        public PostType PostType { get; set; }
        public int Number { get; set; }
    }
}
