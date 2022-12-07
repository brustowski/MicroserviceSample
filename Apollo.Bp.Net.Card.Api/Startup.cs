using System;
using System.Reflection;
using Apollo.Bp.Net.Api.Common;
using Apollo.Bp.Net.Api.Common.Extension;
using Apollo.Bp.Net.Card.Core.Interfaces;
using Apollo.Bp.Net.Card.Core.KafkaMessages;
using Apollo.Bp.Net.Card.Core.MappingProfiles;
using Apollo.Bp.Net.Card.Core.Services;
using Apollo.Bp.Net.Card.Data;
using Apollo.Bp.Net.Card.Infrastructure.Constants;
using Apollo.Bp.Net.Card.Infrastructure.Extensions;
using Apollo.Bp.Net.Card.Infrastructure.Messaging;
using Apollo.Bp.Net.Kafka.Base;
using Apollo.Bp.Net.Kafka.Config;
using Apollo.Bp.Net.Kafka.Consumer;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Apollo.Bp.Net.Card.Api
{
	internal sealed class Startup : ApiStartup
	{
		public Startup(IConfiguration cfg, IHostEnvironment env) : base(cfg, env)
		{
		}

		public override void ConfigureServices(IServiceCollection services)
		{
			services.AddBaseApi(Configuration, Environment);

			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			services.AddApiBrowser(Configuration, Environment, xmlFile);

			var mappingConfig = new MapperConfiguration(mc => mc.AddMaps(typeof(AutomapperProfile).Assembly.ExportedTypes));
			var mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);

			services.AddMarqetaIntegration(Configuration);
			services.AddMambuIntegration(Configuration);
			services.AddTransient<ICardService, CardService>();
			services.AddTransient<IPinService, PinService>();

			RegisterKafkaConsumers(services, Configuration);

			services.AddPaymentDbContext(Configuration);

			services.AddHttpClientHeaders(Configuration, Environment);

			services
				.AddControllers(e =>
				{
					e.Filters.Add<ExceptionFilterAttribute>();
					e.Filters.Add<ValidateModelStateAttribute>();
				})
				.AddApiJson();
		}

		public override void Configure(IApplicationBuilder app)
		{
			app.UseApi();

			if (Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseApiBrowser();

			app.UseHttpClientHeaders();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHealthChecks("/health");
			});

			app.RunDbContextMigration();
		}

		private static void RegisterKafkaConsumers(IServiceCollection services, IConfiguration configuration)
		{
			var kafkaCfg = configuration.GetSection("Kafka").Get<KafkaOptions>();

			services.AddScoped<IKafkaHandler<CardTransitionsMessage>, KafkaHandleCardTransitionNotificationConsumer>();
			services.AddScoped<IKafkaHandler<CardActionsMessage>, KafkaHandleCardActionNotificationConsumer>();

			services.AddHostedService(serviceProvider =>
				CreateKafkaConsumer<CardTransitionsMessage>(serviceProvider, kafkaCfg, KafkaConsumerNameConstants.HandleCardTransitionNotificationConsumer));
			services.AddHostedService(serviceProvider =>
				CreateKafkaConsumer<CardActionsMessage>(serviceProvider, kafkaCfg, KafkaConsumerNameConstants.HandleCardActionNotificationConsumer));
		}

		private static ConsumerBase<T> CreateKafkaConsumer<T>(IServiceProvider serviceProvider, KafkaOptions options, string name) where T : IKafkaMessage
		{
			return new ConsumerBase<T>(
				serviceProvider,
				serviceProvider.GetService<ILogger<ConsumerBase<T>>>(),
				options.GetConsumer(name));
		}
	}
}