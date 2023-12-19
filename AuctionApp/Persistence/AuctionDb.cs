using System.ComponentModel.DataAnnotations;

namespace AuctionApp.Persistence
{
    public class AuctionDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Owner { get; set; }

        public double StartingPrice { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        public IEnumerable<BidDb> BidDbs { get; set; } = new List<BidDb>();
    }
}
