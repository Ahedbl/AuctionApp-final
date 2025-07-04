﻿namespace AuctionApp.Core.Interfaces
{
    public interface IAuctionPersistence
    {
        List<Auction> GetAll(string owner);

        List<Auction> GetMyAuctions(string owner);

        Auction GetById(int id);

        void Add(Auction auction);

        void Add(int id, Bid bid);

        void Edit(int id, string description);
    }
}
