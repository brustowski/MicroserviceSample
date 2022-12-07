using System;
using System.Net.Http.Headers;
using System.Net.Mime;
using Apollo.Bp.Net.Api.Common.Extension;
using Apollo.Bp.Net.Card.Core.Configuration;
using Apollo.Bp.Net.Card.Core.Interfaces;
using Apollo.Bp.Net.Card.Infrastructure.Integrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Apollo.Bp.Net.Card.Infrastructure.Extensions
{
	public static class MarqetaIntegrationExtension
	{
		public static void AddMarqetaIntegration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddServiceIntegration<IMarqetaIntegration, MarqetaIntegration, MarqetaOptions>(
				MarqetaIntegration.HttpClientKey, configuration, (httpClient, cfg) =>
				{
					httpClient.BaseAddress = new Uri($"{cfg.Url}/");
					httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", cfg.ApiKey);
					httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
				});
		}
	}
}