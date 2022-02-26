
using BoardGames.DataAccess.Repository.IRepository;
using BoardGames.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public IActionResult Privacy()
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