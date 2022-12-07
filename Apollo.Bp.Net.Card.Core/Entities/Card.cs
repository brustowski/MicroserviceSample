using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apollo.Bp.Net.Card.Core.Entities
{
	[Table("cards")]
	public class Card
	{
		[Key]
		[Required]
		[Column("id")]
		public Guid Id { get; set; }

		[Required]
		[Column("token")]
		public Guid Token { get; set; }

		[Required]
		[Column("party_id")]
		public Guid PartyId { get; set; }

		[Required]
		[Column("card_product_token")]
		public Guid CardProductToken { get; set; }

		[Required]
		[Column("account_id")]
		public string AccountId { get; set; }

		[Required]
		[Column("pan")]
		public string Pan { get; set; }

		[Required]
		[Column("last_four")]
		public string LastFour { get; set; }

		[Required]
		[Column("expiration")]
		public string Expiration { get; set; }

		[Column("expiration_time")]
		public DateTime? ExpirationTime { get; set; }

		[MaxLength(30)]
		[Column("barcode")]
		public string Barcode { get; set; }

		[Required]
		[Column("pin_is_set")]
		public bool PinIsSet { get; set; }

		[Column("pin_update_at")]
		public DateTime? PinUpdateAt { get; set; }

		[Column("state")]
		public string State { get; set; }

		[Column("state_update_at")]
		public DateTime? StateUpdateAt { get; set; }

		[Column("state_reason")]
		public string StateReason { get; set; }

		[MaxLength(2)]
		[Column("reason_code")]
		public string ReasonCode { get; set; }

		[Column("fulfillment_status")]
		public string FulfillmentStatus { get; set; }

		[Column("instrument_type")]
		public string InstrumentType { get; set; }

		[Column("shipping_method")]
		public string ShippingMethod { get; set; }

		[Column("shipping_care_of_line")]
		public string ShippingCareOfLine { get; set; }

		[Column("return_address1")]
		public string ReturnAddress1 { get; set; }

		[Column("return_address2")]
		public string ReturnAddress2 { get; set; }

		[Column("return_city")]
		public string ReturnCity { get; set; }

		[Column("return_state")]
		public string ReturnState { get; set; }

		[Column("return_postal_code")]
		public string ReturnPostalCode { get; set; }

		[Column("return_country")]
		public string ReturnCountry { get; set; }

		[Column("return_phone")]
		public string ReturnPhone { get; set; }

		[Column("return_first_name")]
		public string ReturnFirstName { get; set; }

		[Column("return_middle_name")]
		public string ReturnMiddleName { get; set; }

		[Column("return_last_name")]
		public string ReturnLastName { get; set; }

		[Column("recipient_address1")]
		public string RecipientAddress1 { get; set; }

		[Column("recipient_address2")]
		public string RecipientAddress2 { get; set; }

		[Column("recipient_city")]
		public string RecipientCity { get; set; }

		[Column("recipient_state")]
		public string RecipientState { get; set; }

		[Column("recipient_postal_code")]
		public string RecipientPostalCode { get; set; }

		[Column("recipient_country")]
		public string RecipientCountry { get; set; }

		[Column("recipient_phone")]
		public string RecipientPhone { get; set; }

		[Column("recipient_first_name")]
		public string RecipientFirstName { get; set; }

		[Column("recipient_middle_name")]
		public string RecipientMiddleName { get; set; }

		[Column("recipient_last_name")]
		public string RecipientLastName { get; set; }

		[Column("perso_type")]
		public string PersoType { get; set; }

		[Column("text_name_line1")]
		public string TextNameLine1 { get; set; }

		[Column("text_name_line2")]
		public string TextNameLine2 { get; set; }

		[Column("text_name_line3")]
		public string TextNameLine3 { get; set; }

		[Column("template_id")]
		public string TemplateId { get; set; }

		[Column("logo_file")]
		public string LogoFile { get; set; }

		[Column("logo_thumbnail_file")]
		public string LogoThumbnailFile { get; set; }

		[Column("message_file")]
		public string MessageFile { get; set; }

		[Column("message_line")]
		public string MessageLine { get; set; }

		[Column("images_card_name")]
		public string ImagesCardName { get; set; }

		[Column("images_card_thermal_color")]
		public string ImagesCardThermalColor { get; set; }

		[Column("images_card_signature")]
		public string ImagesCardSignature { get; set; }

		[Column("carrier_return_window_name")]
		public string CarrierReturnWindowName { get; set; }

		[Column("metadata")]
		public string Metadata { get; set; }

		[Column("expedite")]
		public bool Expedite { get; set; }

		[Column("create_at")]
		public DateTime CreateAt { get; set; }

		[Column("update_at")]
		public DateTime UpdateAt { get; set; }
	}
}