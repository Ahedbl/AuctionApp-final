using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Persistence
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

        public DbSet<AuctionDb> AuctionDbs { get; set; }

        public DbSet<BidDb> BidDbs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AuctionDb adb = new AuctionDb
            {
                Id = -1,
                Name = "Auction 1",
                Description = "Item 1",
                Owner = "test@email.com",
                StartingPrice = 100,
                EndTime = new DateTime(2023, 12, 30, 10, 30, 00),
                BidDbs = new List<BidDb>()
            };
            modelBuilder.Entity<AuctionDb>().HasData(adb);

            BidDb bdb1 = new BidDb()
            {
                Id = -1,
                BidAmount = 150,
                TimeOfBid = new DateTime(2023, 12, 25, 12, 45, 00),
                AuctionId = -1,
                Bidder = "Bill@mail.com"
            };
            BidDb bdb2 = new BidDb()
            {
                Id = -2,
                BidAmount = 200,
                TimeOfBid = new DateTime(2023, 12, 26, 09, 10, 00),
                AuctionId = -1,
                Bidder = "Bill@mail.com"
            };
            modelBuilder.Entity<BidDb>().HasData(bdb1);
            modelBuilder.Entity<BidDb>().HasData(bdb2);
        }
    }
}
