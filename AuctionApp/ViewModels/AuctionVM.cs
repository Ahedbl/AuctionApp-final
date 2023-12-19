using AuctionApp.Core;

namespace AuctionApp.ViewModels
{
    public class AuctionVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double StartingPrice { get; set; }

        public DateTime EndTime { get; set; }

        public static AuctionVM FromAuction(Auction auction)
        {
            return new AuctionVM()
            {
                Id = auction.Id,
                Name = auction.Name,
                Description = auction.Description,
                StartingPrice = auction.StartingPrice,
                EndTime = auction.EndTime
            };
        }
    }
}
