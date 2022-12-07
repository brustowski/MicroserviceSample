using System;
using Apollo.Bp.Net.Api.Common.Extension;
using Apollo.Bp.Net.Card.Core.Configuration;
using Apollo.Bp.Net.Card.Core.Interfaces;
using Apollo.Bp.Net.Card.Infrastructure.Integrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Apollo.Bp.Net.Card.Infrastructure.Extensions
{
	public static class MambuIntegrationExtension
	{
		public static void AddMambuIntegration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddServiceIntegration<IMambuIntegration, MambuIntegration, MambuOptions>(
				MambuIntegration.HttpClientKey, configuration, (httpClient, cfg) =>
				{
					httpClient.BaseAddress = new Uri($"{cfg.Url}/");
					httpClient.DefaultRequestHeaders.Add("ApiKey", cfg.ApiKey);
					httpClient.DefaultRequestHeaders.Add("Accept", cfg.Accept);
				});
		}
	}
}