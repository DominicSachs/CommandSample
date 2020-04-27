using System.Threading.Tasks;
using CommandSample.Domain.Actions.Base;

namespace CommandSample.Server.ActionHandlers.Base
{
	internal abstract class AbstractActionHandler<TResult>
	{
		internal abstract string ActionName { get; }

		internal abstract Task<TResult> Execute(AbstractAction<TResult> action);
	}

	internal abstract class AbstractActionHandler<TAction, TResult> : AbstractActionHandler<TResult> where TAction : AbstractAction<TResult>
	{
		internal sealed override string ActionName => typeof(TAction).Name;

		internal sealed override async Task<TResult> Execute(AbstractAction<TResult> action)
		{
			return await ExecuteInternal(action as TAction);
		}

		protected abstract Task<TResult> ExecuteInternal(TAction action);
	}
}
