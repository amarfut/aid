using Domain.Entities;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Queries
{
    public class GetPostQuery : IQuery<PostDto>
    {
        public string Url { get; set; }

        public GetPostQuery(string url) => Url = url;
    }
}
