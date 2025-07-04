﻿using AuctionApp.Core.Interfaces;

namespace AuctionApp.Core
{
    public class AuctionService : IAuctionService
    {
        private IAuctionPersistence _auctionPersistence;

        public AuctionService(IAuctionPersistence auctionPersistence)
        {
            _auctionPersistence = auctionPersistence;
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
