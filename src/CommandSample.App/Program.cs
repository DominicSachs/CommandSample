using System.Threading.Tasks;
using CommandSample.Domain.Actions;
using CommandSample.Domain.Execution;
using CommandSample.Domain.Models;
using CommandSample.Server.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CommandSample.App
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var actionExecutor = InitializeAndGetExecutor();

			await actionExecutor.Execute(new HelloAction(new ActionCallContext(), $"from {typeof(Program).Assembly.GetName()}"));
		}

		private static IActionExecutor InitializeAndGetExecutor()
		{
			var serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);

			var serviceProvider = serviceCollection.BuildServiceProvider();

			return serviceProvider.GetRequiredService<IActionExecutor>();
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging(configure => configure.AddConsole()).Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);
			services.AddAppServer();
		}
	}
}
