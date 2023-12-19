using System.ComponentModel.DataAnnotations;

namespace AuctionApp.ViewModels
{
    public class EditAuctionVM
    {
        [StringLength(128, ErrorMessage = "Max length 128 characters")]
        public string Description { get; set; }
    }
}
