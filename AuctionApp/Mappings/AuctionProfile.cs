using AuctionApp.Core;
using AuctionApp.Persistence;
using AutoMapper;

namespace AuctionApp.Mappings
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            CreateMap<AuctionDb, Auction>()
                .ReverseMap();
        }
    }
}
