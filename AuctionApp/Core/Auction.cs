namespace AuctionApp.Core
{
    public class Auction
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        public double StartingPrice { get; set; }

        public DateTime EndTime { get; set; }

        private List<Bid> _bids = new List<Bid>();

        public IEnumerable<Bid> Bids => _bids;

        public Auction() { }

        public Auction(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Auction(string name, DateTime endTime)
        {
            Name = name;
            EndTime = endTime;
        }

        public Auction(int id, string name, string description, double startingPrice, DateTime endTime)
        {
            Id = id;
            Name = name;
            Description = description;
            StartingPrice = startingPrice;
            EndTime = endTime;
        }

        public void AddBid(Bid newBid)
        {
            _bids.Add(newBid);
        }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }
}
