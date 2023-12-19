namespace AuctionApp.Core
{
    public class Bid
    {
        public int Id { get; set; }

        public string Bidder { get; set; }

        public double BidAmount { get; set; }

        public DateTime TimeOfBid { get; set; }

        public Bid() { }

        public Bid(int id, double bidAmount)
        {
            Id = id;
            BidAmount = bidAmount;
        }

        public Bid(string bidder, double bidAmount, DateTime timeOfBid)
        {
            Bidder = bidder;
            BidAmount = bidAmount;
            TimeOfBid = timeOfBid;
        }

        public Bid(int id, string bidder, double bidAmount, DateTime timeOfBid)
        {
            Id = id;
            Bidder = bidder;
            BidAmount = bidAmount;
            TimeOfBid = timeOfBid;
        }

        public override string ToString()
        {
            return $"{Id}: {Bidder}";
        }
    }
}
