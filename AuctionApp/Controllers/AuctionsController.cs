using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AuctionApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AuctionApp.Controllers
{
    [Authorize]
    public class AuctionsController : Controller
    {
        private readonly IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // GET: AuctionsController
        public ActionResult Index()
        {
            string owner = User.Identity.Name;
            List<Auction> auctions = _auctionService.GetAll(owner);
            List<AuctionVM> auctionVMs = new();
            foreach(var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }
            return View(auctionVMs);
        }

        // GET: AuctionsController
        public ActionResult IndexMyAuctions()
        {
            string owner = User.Identity.Name;
            List<Auction> auctions = _auctionService.GetMyAuctions(owner);
            List<AuctionVM> auctionVMs = new();
            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }
            return View(auctionVMs);
        }

        // GET: AuctionsController/Details/5
        public ActionResult Details(int id)
        {
            Auction auction = _auctionService.GetById(id);
            if(auction == null)
            {
                return NotFound();
            }
            AuctionDetailsVM detailsVM = AuctionDetailsVM.FromAuction(auction);
            return View(detailsVM);
        }

        
        // GET: AuctionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuctionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAuctionVM vm)
        {
            if(ModelState.IsValid)
            {
                Auction auction = new Auction()
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    StartingPrice = vm.StartingPrice,
                    EndTime = vm.EndTime,
                    Owner = User.Identity.Name
                };
                _auctionService.Add(auction);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: AuctionsController/CreateBid
        public ActionResult CreateBid()
        {
            return View();
        }

        // POST: AuctionsController/CreateBid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBid(int id, CreateBidVM vm)
        {
            if (ModelState.IsValid)
            {
                Auction auction = _auctionService.GetById(id);
                Bid bid = new Bid()
                {
                    BidAmount = vm.BidAmount
                };
                _auctionService.Add(auction.Id, bid);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: AuctionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuctionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditAuctionVM vm)
        {
            if(ModelState.IsValid)
            {
                Auction auction = _auctionService.GetById(id);
                auction.Description = vm.Description;
                _auctionService.Edit(auction.Id, auction.Description); 
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        /*
        // GET: AuctionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuctionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
