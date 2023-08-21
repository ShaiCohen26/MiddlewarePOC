using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewarePOC.Commands.Common;

interface ICommandDispatcher
{
	Task Dispatch<TCommand>(TCommand command, CancellationToken cancellation);
}