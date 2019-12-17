using Domain.Entities;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Queries
{
    public class GetUserBookmarksQuery : IQuery<List<PostPreviewDto>>
    {
        public string UserId { get; set; }

        public GetUserBookmarksQuery(string userId) => UserId = userId;
    }
}
