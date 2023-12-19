using AuctionApp.Core.Interfaces;

namespace AuctionApp.Core
{
    public class MockAuctionService /* : IAuctionService */
    {
        public List<Auction> GetAll()
        {
            Auction a1 = new Auction(1, "Item 1");
            Auction a2 = new Auction(2, "Item 2");
            a2.AddBid(new Core.Bid(1, 200));
            a2.AddBid(new Core.Bid(1, 300));
            List<Auction> auctions = new();
            auctions.Add(a1);
            auctions.Add(a2);
            return auctions;
        }
    }
}
