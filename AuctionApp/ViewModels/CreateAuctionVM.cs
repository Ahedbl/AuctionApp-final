using System.ComponentModel.DataAnnotations;

namespace AuctionApp.ViewModels
{
    public class CreateAuctionVM
    {
        [Required]
        [StringLength(64, ErrorMessage = "Max length 64 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Max length 128 characters")]
        public string Description { get; set; }

        [Required]
        public double StartingPrice { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
