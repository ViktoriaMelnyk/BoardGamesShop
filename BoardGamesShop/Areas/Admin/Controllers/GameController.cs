
using BoardGames.DataAccess;
using BoardGames.DataAccess.Repository.IRepository;
using BoardGames.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.Controllers
{[Area("Admin")]
    public class GameController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IWebHostEnvironment _hostEnviroment;



        public GameController(IUnitOfWork db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnviroment = hostEnvironment;
        }
        public IActionResult Index()
        {
            //Return a list to the view
            IEnumerable<Category> categoryList = _db.Category.GetAll();

            return View(categoryList);
        }
        
        //GET
        public IActionResult Upsert(int? id)
        {
            Game game = new();
            IEnumerable<SelectListItem> CategoryList = _db.Category.GetAll().Select(
                u =>new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }
            );
            if (id == null || id == 0)
            {
                //create
                ViewBag.CategoryList=CategoryList;
                return View(game);
                
            }
            else
            {
                //update
            }

            return View(game);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Game obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"img\gamesImg");
                    var extension = Path.GetExtension(file.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads,fileName+ extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.ImageUrl = @"\img\gamesImg\" + fileName + extension;
                }
                _db.Game.Add(obj);
                _db.Save();
                TempData["success"] = "Gra dodana pomyślnie";
                return RedirectToAction("Index");

            }
            return View(obj);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var catFromDb = _db.Category.GetFirstOrDefault(u => u.Id == id);
            if (catFromDb == null)
            {

                return NotFound();

            }
            return View(catFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var catFromDb = _db.Category.GetFirstOrDefault(u => u.Id == id);
            if (catFromDb == null)
            {

                return NotFound();

            }


            _db.Category.Remove(catFromDb);
            _db.Save();
            TempData["success"] = "Kategoria usunięta pomyślnie";
            return RedirectToAction("Index");

            
            
        }
    }
}
