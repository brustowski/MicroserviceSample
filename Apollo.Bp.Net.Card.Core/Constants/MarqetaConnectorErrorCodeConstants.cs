namespace Apollo.Bp.Net.Card.Core.Constants
{
	public static class MarqetaConnectorErrorCodeConstants
	{
		public const string GeneralError = "marqeta_general_error";
		public const string InternalError = "marqeta_internal_error";
		public const string InvalidInputParameter = "marqeta_invalid_input_parameter";

		public const string ClientExist = "marqeta_client_already_exist";
		public const string ClientNotFound = "marqeta_client_not_found";

		public const string CardNotFound = "marqeta_card_not_found";
		public const string MalformedJsonRequest = "marqeta_malformed_json_request";
		public const string CardProductNotFound = "marqeta_card_product_not_found";
		public const string CardHolderExist = "marqeta_card_holder_already_exist";

		public const string ControlTokenIsInvalid = "marqeta_invalid_control_token";
		public const string PinIsInvalid = "marqeta_invalid_pin";
	}
}