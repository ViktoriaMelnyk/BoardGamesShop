
using BoardGames.DataAccess;
using BoardGames.DataAccess.Repository.IRepository;
using BoardGames.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.Controllers
{[Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _db;



        public CategoryController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //Return a list to the view
            IEnumerable<Category> categoryList = _db.Category.GetAll();

            return View(categoryList);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Nazwa kategorii nie może być taka sama jak kolejność wyświetlania.");
            }
            if (ModelState.IsValid)
            {            
                _db.Category.Add(obj);
                _db.Save();
                TempData["success"] = "Kategoria utworzona pomyślnie";
                return RedirectToAction("Index");

            }
            return View(obj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var catFromDb = _db.Category.GetFirstOrDefault(u=>u.Id==id);
            if (catFromDb == null)
            {

                return NotFound();

            }
            return View(catFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Nazwa kategorii nie może być taka sama jak kolejność wyświetlania.");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Update(obj);
                _db.Save();
                TempData["success"] = "Kategoria edytowana pomyślnie";
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
