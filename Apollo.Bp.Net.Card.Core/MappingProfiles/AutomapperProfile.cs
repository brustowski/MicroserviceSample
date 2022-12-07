using Apollo.Bp.Net.Card.Core.DTOs.Cards;
using Apollo.Bp.Net.Card.Core.DTOs.Requests;
using Apollo.Bp.Net.Card.Core.DTOs.Responses;
using AutoMapper;

namespace Apollo.Bp.Net.Card.Core.MappingProfiles
{
	public class AutomapperProfile : Profile
	{
		public AutomapperProfile()
		{
			CreateMap<VirtualCardInputModel, UserInputModel>()
				.ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.ClientId))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));

			CreateMap<VirtualCardInputModel, CardInputModel>()
				.ForMember(dest => dest.CardProductToken, opt => opt.MapFrom(src => src.CardProductId))
				.ForMember(dest => dest.UserToken, opt => opt.MapFrom(src => src.ClientId));

			CreateMap<MarqetaConnectorCardResponse, Entities.Card>()
				.ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Data.Token))
				.ForMember(dest => dest.PartyId, opt => opt.MapFrom(src => src.Data.UserToken))
				.ForMember(dest => dest.CardProductToken, opt => opt.MapFrom(src => src.Data.CardProductToken))
				.ForMember(dest => dest.LastFour, opt => opt.MapFrom(src => src.Data.LastFour))
				.ForMember(dest => dest.Pan, opt => opt.MapFrom(src => src.Data.Pan))
				.ForMember(dest => dest.Expiration, opt => opt.MapFrom(src => src.Data.Expiration))
				.ForMember(dest => dest.ExpirationTime, opt => opt.MapFrom(src => src.Data.ExpirationTime))
				.ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Data.BarCode))
				.ForMember(dest => dest.PinIsSet, opt => opt.MapFrom(src => src.Data.PinIsSet))
				.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Data.State))
				.ForMember(dest => dest.StateReason, opt => opt.MapFrom(src => src.Data.StateReason))
				.ForMember(dest => dest.FulfillmentStatus, opt => opt.MapFrom(src => src.Data.FulfillmentStatus))
				.ForMember(dest => dest.InstrumentType, opt => opt.MapFrom(src => src.Data.InstrumentType));

			CreateMap<Entities.Card, VirtualCardResponse>()
				.ForMember(dest => dest.CardId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.LastFour, opt => opt.MapFrom(src => src.LastFour))
				.ForMember(dest => dest.Pan, opt => opt.MapFrom(src => src.Pan))
				.ForMember(dest => dest.Expiration, opt => opt.MapFrom(src => src.Expiration))
				.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));

			CreateMap<SetCardPinInputModel, SetPinInputModel>();

			CreateMap<Entities.Card, CardDto>()
				.ForMember(dest => dest.CardId, opt => opt.MapFrom(src => src.Token))
				.ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.PartyId))
				.ForMember(dest => dest.CardProductId, opt => opt.MapFrom(src => src.CardProductToken))
				.ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountId));

			CreateMap<ChangeCardStatusMarqetaResponse, ChangeCardStatusResponse>()
				.ForMember(dest => dest.CardId, opt => opt.MapFrom(src => src.CardToken));

			CreateMap<CardDataInputModel, Entities.Card>()
				.ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.CardId))
				.ForMember(dest => dest.CardProductToken, opt => opt.MapFrom(src => src.CardProductId));
		}
	}
}