using Domain.Commands;
using Services.DTOs;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Services.CommandHandlers
{
    public interface ICommandHandler<TInput, TOutput> where TInput : IDomainCommand
    {
        Task<TOutput> HandleAsync(TInput command);
    }
}
