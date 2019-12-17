using System;

namespace Domain.Commands
{
    public class CreateUserCommand : IDomainCommand
    {
        public CreateUserCommand(string externalId, string name, string provider, string userPictureUrl)
        {
            ExternalId = externalId;
            Name = name;
            Provider = provider;
            PhotoUrl = userPictureUrl;
        }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public string Provider { get; set; }
        public string PhotoUrl { get; set; }
    }
}
