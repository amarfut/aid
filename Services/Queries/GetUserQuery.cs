using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Queries
{
    public class GetUserQuery : IQuery<UserDto>
    {
        public string UserId { get; set; }
    }
}
