using System;

namespace Domain.Commands
{
    public class UpdateProfileCommand : IDomainCommand
    {
        public string Username { get; set; }

        public string UserId { get; set; }
    }
}
