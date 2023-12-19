using AuctionApp.Core.Interfaces;
using AuctionApp.Persistence;
using AuctionApp.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AuctionApp.Core
{
    public class AuctionService : IAuctionService
    {
        private AuctionDbContext _dbContext;

        private IMapper _mapper;
        private IAuctionPersistence _auctionPersistence;

        public Boolean GetWonAuctions(int id, string owner)
        {
            return _auctionPersistence.GetWonAuctions(id, owner);
        }

        public AuctionService(IAuctionPersistence auctionPersistence)
        {
            _auctionPersistence = auctionPersistence;
        }
        public Boolean GetMyBids(int id, string owner)
        {
            Boolean ownerBids = false;
            // Eager loading
            var auctionDb = _dbContext.AuctionDbs
                .Include(a => a.BidDbs.OrderByDescending(a => a.BidAmount))
                .Where(a => a.Id == id)
                .SingleOrDefault();

            Auction auction = _mapper.Map<Auction>(auctionDb);

            foreach (BidDb bdb in auctionDb.BidDbs)
            {

                auction.AddBid(_mapper.Map<Bid>(bdb));
                if (bdb.Bidder == owner && auction.EndTime > DateTime.UtcNow) ownerBids = true;
            }
            return ownerBids;
        }

        public List<Auction> GetAll(string owner)
        {
            return _auctionPersistence.GetAll(owner);
        }

        public List<Auction> GetMyAuctions(string owner)
        {
            return _auctionPersistence.GetMyAuctions(owner);
        }

        public Auction GetById(int id)
        {
            return _auctionPersistence.GetById(id);
        }

        public void Add(Auction auction)
        {
            if(auction == null || auction.Id != 0)
            {
                throw new InvalidDataException();
            }
            _auctionPersistence.Add(auction);
        }

        public void Add(int id, Bid bid)
        {
            Auction auction = _auctionPersistence.GetById(id);

            if(auction.Bids.Count() != 0 && bid.BidAmount < auction.Bids.Max(b => b.BidAmount) || bid.BidAmount < auction.StartingPrice)
            {
                return;
            }

            bid.TimeOfBid = DateTime.Now;
            _auctionPersistence.Add(id, bid);
        }

        public void Edit(int id, string description)
        {
            _auctionPersistence.Edit(id, description);
        }
    }
}
