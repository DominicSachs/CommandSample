using CommandSample.Domain.Execution;
using CommandSample.Server.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace CommandSample.Server.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddAppServer(this IServiceCollection services)
		{
			services.AddScoped<IActionExecutor, ActionExecutor>();
		}
	}
}
