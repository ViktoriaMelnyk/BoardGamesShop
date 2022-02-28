using BoardGames.DataAccess.Repository.IRepository;
using BoardGames.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BoardGamesShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitofWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public ShoppingCartController( IUnitOfWork unitOfWork)
        {
           
            _unitofWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM() 
            { 
                ListCart = _unitofWork.ShoppingCart.GetAll(u=>u.ApplicationUserId==claim.Value, includeProperties: "Game")
            };
            foreach(var item in ShoppingCartVM.ListCart)
            {
                item.Price = GetPrice(item.Count, item.Game.Price, item.Game.Price3, item.Game.Price10);
                ShoppingCartVM.CartTotal +=(item.Price * item.Count);
            }
            return View(ShoppingCartVM);
        }
        private double GetPrice(double quantity, double price, double price3, double price10)
        {
            if (quantity <= 3)
            {
                return price;
            }
            else
            {
                if(quantity<=10)
                {
                    return price3;
                }
                return price10;
            }
        }

    }
}
