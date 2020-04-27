using System.Threading;
using System.Threading.Tasks;
using CommandSample.Domain.Actions.Base;

namespace CommandSample.Domain.Execution
{
	public interface IActionExecutor
	{
		Task<TResult> Execute<TResult>(AbstractAction<TResult> action, CancellationToken cancellationToken = default);
	}
}