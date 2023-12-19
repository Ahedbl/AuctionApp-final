using AuctionApp.Core;

namespace AuctionApp.ViewModels
{
    public class BidVM
    {
        public int Id { get; set; }

        public double BidAmount { get; set; }

        public DateTime TimeOfBid { get; set; }
        public string Bidder {  get; set; }

        public static BidVM FromBid(Bid bid)
        {
            return new BidVM()
            {
                Id = bid.Id,
                BidAmount = bid.BidAmount,
                TimeOfBid = bid.TimeOfBid,
                Bidder = bid.Bidder
            };
        }
    }
}
