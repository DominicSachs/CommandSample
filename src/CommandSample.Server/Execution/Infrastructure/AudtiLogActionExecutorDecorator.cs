using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CommandSample.Domain.Actions.Base;
using CommandSample.Domain.Execution;
using Microsoft.Extensions.Logging;

namespace CommandSample.Server.Execution.Infrastructure
{
	public class AudtiLogActionExecutorDecorator : IActionExecutor
	{
		private readonly IActionExecutor _decoratee;
		private readonly ILogger<AudtiLogActionExecutorDecorator> _logger;

		public AudtiLogActionExecutorDecorator(IActionExecutor decoratee, ILogger<AudtiLogActionExecutorDecorator> logger)
		{
			_decoratee = decoratee;
			_logger = logger;
		}
		public Task<TResult> Execute<TResult>(AbstractAction<TResult> action, CancellationToken cancellationToken = default)
		{
			var result = _decoratee.Execute(action, cancellationToken);
			AppendToAuditTrail(action);
			return result;
		}

		private void AppendToAuditTrail<TResult>(AbstractAction<TResult> action)
		{
			var entry = new 
			{
				UserId = action.Context.UserGuid,
				Operation = action.Name,
				Data = JsonSerializer.Serialize(action)
			};

			_logger.LogInformation("Log Audit: {logEntry}", JsonSerializer.Serialize(entry));
		}
	}
}
