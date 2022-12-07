using System.Collections.Generic;
using Apollo.Bp.Net.Api.Common.Configuration.Integration;
using Apollo.Bp.Net.Card.Core.Extensions;

namespace Apollo.Bp.Net.Card.Core.Configuration
{
	public class MarqetaOptions : IServiceConfiguration
	{
		private string _url;

		public string Url
		{
			get => _url;
			set => _url = value.TrimEndingSlashes();
		}

		public string ApiKey { get; set; }

		public int RequestLifetime { get; set; }

		public int HandlerLifetime { get; set; }

		public List<string> ProhibitedPins { get; set; }
	}
}