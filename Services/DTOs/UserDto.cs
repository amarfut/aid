using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
    }
}
