using Domain.Entities;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Queries
{
    public class GetPostPreviewQuery : IQuery<List<PostPreviewDto>>
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public GetPostPreviewQuery(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
