using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandSample.Domain.Actions.Base;
using CommandSample.Domain.Execution;
using CommandSample.Domain.Security;
using CommandSample.Server.ActionHandlers.Base;
using CommandSample.Server.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CommandSample.Server.Execution
{
    internal sealed class ActionExecutor : IActionExecutor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ActionExecutor> _logger;
        private static readonly ConcurrentDictionary<string, Type> _actionHandlers = new ConcurrentDictionary<string, Type>();

        public ActionExecutor(IServiceProvider serviceProvider, ILogger<ActionExecutor> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        static ActionExecutor()
        {
            var baseType = typeof(AbstractActionHandler<,>);

            foreach (var type in baseType.Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsAssignableToGenericType(baseType)))
            {
                var actionName = type.BaseType.GenericTypeArguments[0].Name;
                _actionHandlers.AddOrUpdate(actionName, type, (key, oldValue) => type);
            }
        }

        public Task<TResult> Execute<TResult>(AbstractAction<TResult> action, CancellationToken cancellationToken = default)
        {
            ThrowIf.Argument.IsNull(action, nameof(action));
            ThrowIf.Value.IsFalse(_actionHandlers.TryGetValue(action.Name, out var handlerType), $"No handler for {action.Name} found.");
            
            var handler = (AbstractActionHandler<TResult>)ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
            var handlerName = handler.GetType().Name;

            _logger.LogInformation("Start executing handler {ActionHandler} for action {ActionName}.", handlerName, handler.ActionName);

            var result = handler.Execute(action);

            _logger.LogInformation("End executing handler {ActionHandler} for action {ActionName}.", handlerName, handler.ActionName);

            return result;
        }
    }
}