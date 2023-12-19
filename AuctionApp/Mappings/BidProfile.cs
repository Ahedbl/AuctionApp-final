using AuctionApp.Core;
using AuctionApp.Persistence;
using AutoMapper;

namespace AuctionApp.Mappings
{
    public class BidProfile : Profile
    {
        public BidProfile()
        {
            CreateMap<BidDb, Bid>()
                .ReverseMap();
        }
    }
}
