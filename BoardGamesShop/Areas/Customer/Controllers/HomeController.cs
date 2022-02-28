
using BoardGames.DataAccess.Repository.IRepository;
using BoardGames.Models;
using BoardGames.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BoardGamesShop.Controllers
{[Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitofWork;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitofWork = unitOfWork; 
        }

        public IActionResult Index()
           
        {
            IEnumerable<Game> gamesList = _unitofWork.Game.GetAll(includeProperties:"Category");
            return View(gamesList);
        }
        public IActionResult Details(int gameid)

        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                GameId = gameid,
                Game = _unitofWork.Game.GetFirstOrDefault(u => u.Id == gameid, includeProperties: "Category")
            };
             return View(cartObj);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)

        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;
            ShoppingCart cart = _unitofWork.ShoppingCart.GetFirstOrDefault(
                u=>u.ApplicationUserId==claim.Value && u.GameId==shoppingCart.GameId);

            if (cart == null)
            {
                _unitofWork.ShoppingCart.Add(shoppingCart);

            }
            else
            {
                _unitofWork.ShoppingCart.IncrementCount(cart, shoppingCart.Count);
            }

            
            _unitofWork.Save();
            return RedirectToAction("Index");


        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Message()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}