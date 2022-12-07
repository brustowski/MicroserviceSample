namespace Apollo.Bp.Net.Card.Core.Constants
{
	public static class ErrorCodeConstants
	{
		public const string InternalError = "internal_server_error";
		public const string CardDataSavingError = "card_data_saving_error";
		public const string InvalidInputParametersError = "card_invalid_input_parameters";
		public const string InvalidFilterParametersError = "card_invalid_filter_parameters";

		public const string CardNotFoundError = "card_not_found_error";
		public const string CardHolderAlreadyExistError = "card_cardholder_already_exist";
		public const string ClientAlreadyExistError = "card_client_already_exist";

		public const string CardCreatingError = "card_creating_error";
		public const string CardReferenceCreatingError = "card_reference_creating_error";
		public const string CardClientCreatingError = "card_client_creating_error";

		public const string PinSetError = "card_pin_set_error";
		public const string NotAllowedPinError = "card_not_allowed_pin_error";
		public const string InvalidPinError = "card_invalid_pin_error";

		public const string MarqetaConnectorError = "card_marqeta_connector_call_error";
		public const string MambuIntegrationError = "card_mambu_integration_call_error";
		public const string InvalidCardBlockingStatusError = "card_invalid_blocking_status_error";
		public const string InvalidCardUnblockingStatusError = "card_invalid_unblocking_status_error";
		public const string ReasonCodeIsInvalidError = "card_reason_code_is_invalid";
		public const string StateOrChannelIsInvalidError = "card_value_state_or_channel_is_invalid";
	}
}