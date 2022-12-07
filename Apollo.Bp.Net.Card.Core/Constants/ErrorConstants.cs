namespace Apollo.Bp.Net.Card.Core.Constants
{
	public static class ErrorConstants
	{
		public const string EmptyRequestIdError = "x-request-id can’t be empty.";
		public const string EmptyReferenceIdError = "x-reference-id can’t be empty.";
		public const string EmptyCardIdError = "card_id can’t be empty.";

		public const string NullParameter = "Invalid input(s): {0} may not be null.";
		public const string InvalidInputParametersError = "Invalid input parameters.";
		public const string InternalError = "Something went wrong.";
		public const string MambuIntegrationError = "Error while calling mambu.";
		public const string MarqetaConnectorError = "Error while calling marqeta connector.";
		public const string GeneralError = "General error, try again.";

		public const string ClientAlreadyExistError = "Client has already exist.";
		public const string CardHolderAlreadyExistError = "A card holder with the same email already exist.";
		public const string CardCreatingError = "Card wasn`t created, try again.";
		public const string PinSetError = "Pin wasn't set, try again.";
		public const string InvalidPinError = "Invalid PIN, try again.";

		public const string CardReferenceCreatingError = "Card reference wasn`t created, try again.";
		public const string CardClientCreatingError = "Client wasn`t created, try again.";

		public const string CardNotFoundError = "Card not found.";
		public const string NotAllowedPinError = "Not allowed PIN value, please specify another value.";
		public const string CardDataSavingError = "Card data wasn`t saved, try again.";

		public const string InvalidCardFilterParametersError = "At least one filter parameter must be filled in.";
		public const string InvalidCardBlockingStatusError = "Invalid card status, can not block the card.";
		public const string InvalidCardUnblockingStatusError = "Invalid card status, can not unblock the card.";
		public const string ReasonCodeIsInvalidError = "Value reason_code is invalid";
		public const string StateOrChannelIsInvalidError = "Value state or channel is invalid";
	}
}