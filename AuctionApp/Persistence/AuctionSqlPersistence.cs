using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

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
            var currentTime = DateTime.Now;

            var auctionDbs = _dbContext.AuctionDbs
                .Where(a => a.Owner != owner && a.EndTime > currentTime)
                .OrderByDescending(a => a.EndTime)
                .ToList();

            List<Auction> result = _mapper.Map<List<Auction>>(auctionDbs);

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
        public List<Auction> GetMyBids(string owner)
        {
            var currentTime = DateTime.Now;
            var auctionDbs = _dbContext.AuctionDbs
                .Where(a => a.BidDbs.Any(b => b.Bidder == owner) && a.EndTime > currentTime)
                .ToList();
            List<Auction> result = new List<Auction>();
            foreach (AuctionDb adb in auctionDbs)
            {
                Auction auction = _mapper.Map<Auction>(adb);
                result.Add(auction);
            }
            return result;
        }
        public List<Auction> GetWonAuctions(string owner)
        {
            DateTime currentTime = DateTime.Now;

            var wonAuctions = _dbContext.AuctionDbs
                .Where(a => a.EndTime < currentTime 
                    && a.BidDbs.Any(b => b.Bidder == owner 
                        && b.BidAmount == a.BidDbs.Max(maxBid => maxBid.BidAmount)))
                .ToList();

            List<Auction> result = _mapper.Map<List<Auction>>(wonAuctions);

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
        public Auction MyBids(int id)
        {
            // Eager loading
            var auctionDb = _dbContext.AuctionDbs
                .Include(a => a.BidDbs.OrderByDescending(a => a.BidAmount))
                .Where(a => a.Id == id)
                .SingleOrDefault();

            Auction auction = _mapper.Map<Auction>(auctionDb);
            foreach (BidDb bdb in auctionDb.BidDbs)
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
            auctionDb.Description = description;
            _dbContext.SaveChanges();
        }
    }
}
