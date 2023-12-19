using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionApp.Persistence
{
    public class BidDb
    {
        [Key]
        public int Id { get; set; }

        public double BidAmount { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime TimeOfBid { get; set; }

        [ForeignKey("AuctionId")]
        public AuctionDb AuctionDb { get; set; }

        public int AuctionId { get; set; }
    }
}
