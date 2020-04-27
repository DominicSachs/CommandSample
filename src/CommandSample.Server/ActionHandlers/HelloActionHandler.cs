using System.Threading.Tasks;
using CommandSample.Domain.Actions;
using CommandSample.Domain.Models;
using CommandSample.Server.ActionHandlers.Base;
using Microsoft.Extensions.Logging;

namespace CommandSample.Server.ActionHandlers
{
	internal class HelloActionHandler : AbstractActionHandler<HelloAction, Nothing>
	{
		private readonly ILogger<HelloActionHandler> _logger;

		public HelloActionHandler(ILogger<HelloActionHandler> logger)
		{
			_logger = logger;
		}

		protected override Task<Nothing> ExecuteInternal(HelloAction action)
		{
			_logger.LogInformation($"Hello {action.Text}");

			return Nothing.Task;
		}
	}
}
