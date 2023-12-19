using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Persistence
{
    public class AuctionSqlPersistence : IAuctionPersistence
    {
        private AuctionDbContext _dbContext;
        private IMapper _mapper;

        public AuctionSqlPersistence(AuctionDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<Auction> GetAll(string owner)
        {
            var auctionDbs = _dbContext.AuctionDbs
                .Where(a => a.Owner != owner)
                .ToList();

            List<Auction> result = new List<Auction>();
            foreach(AuctionDb adb in auctionDbs)
            {
                Auction auction = _mapper.Map<Auction>(adb);
                result.Add(auction);
            }
            return result;
        }

        public List<Auction> GetMyAuctions(string owner)
        {
            var auctionDbs = _dbContext.AuctionDbs
                .Where(a => a.Owner.Equals(owner))
                .ToList();

            List<Auction> result = new List<Auction>();
            foreach (AuctionDb adb in auctionDbs)
            {
                Auction auction = _mapper.Map<Auction>(adb);
                result.Add(auction);
            }
            return result;
        }

        public Auction GetById(int id)
        {
            // Eager loading
            var auctionDb = _dbContext.AuctionDbs
                .Include(a => a.BidDbs.OrderByDescending(a => a.BidAmount))
                .Where(a => a.Id == id)
                .SingleOrDefault();

            Auction auction = _mapper.Map<Auction>(auctionDb);
            foreach(BidDb bdb in auctionDb.BidDbs)
            {
                auction.AddBid(_mapper.Map<Bid>(bdb));
            }
            return auction;
        }

        public void Add(Auction auction)
        {
            AuctionDb adb = _mapper.Map<AuctionDb>(auction);
            _dbContext.AuctionDbs.Add(adb);
            _dbContext.SaveChanges();
        }

        public void Add(int id, Bid bid)
        {
            BidDb bdb = _mapper.Map<BidDb>(bid);
            bdb.AuctionId = id;
            _dbContext.BidDbs.Add(bdb);
            _dbContext.SaveChanges();
        }

        public void Edit(int id, string description)
        {
            var auctionDb = _dbContext.AuctionDbs.Find(id);
            auctionDb.Description = description; // Add check for null?
            _dbContext.SaveChanges();
        }
    }
}
