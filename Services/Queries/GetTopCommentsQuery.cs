using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Queries
{
    public class GetTopCommentsQuery : IQuery<List<CommentPreviewDto>>
    {
        public GetTopCommentsQuery(int number = 7) => Number = number;

        public int Number { get; set; } 
    }
}
