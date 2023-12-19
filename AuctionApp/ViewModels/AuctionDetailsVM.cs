using AuctionApp.Core;

namespace AuctionApp.ViewModels
{
    public class AuctionDetailsVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double StartingPrice { get; set; }

        public DateTime EndTime { get; set; }

        public List<BidVM> BidVMs { get; set; } = new();

        public static AuctionDetailsVM FromAuction(Auction auction)
        {
            var detailsVM = new AuctionDetailsVM()
            {
                Id = auction.Id,
                Name = auction.Name,
                Description = auction.Description,
                StartingPrice = auction.StartingPrice,
                EndTime = auction.EndTime
            };
            foreach(var bid in auction.Bids)
            {
                detailsVM.BidVMs.Add(BidVM.FromBid(bid));
            }
            return detailsVM;
        }
    }
}
