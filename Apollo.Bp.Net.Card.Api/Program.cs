using Apollo.Bp.Net.Api.Common.Extension;
using Apollo.Bp.Net.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Apollo.Bp.Net.Card.Api
{
	internal static class Program
	{
		internal static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		private static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
			{
				var builder = webBuilder.UseStartup<Startup>();

				builder.SetupApiConfiguration(args);
				builder.SetupApiLogging(args);
			});
		}
	}
}