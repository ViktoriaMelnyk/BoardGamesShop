using BoardGamesShop.Data;
using BoardGamesShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;



        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //Return a list to the view
            IEnumerable<Category> categoryList = _db.Categories.ToList();

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
                _db.Categories.Add(obj);
                _db.SaveChanges();
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
            var catFromDb = _db.Categories.Find(id);
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
                _db.Categories.Update(obj);
                _db.SaveChanges();
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
            var catFromDb = _db.Categories.Find(id);
            if (catFromDb == null)
            {

                return NotFound();

            }
            return View(catFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

            
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            
            
        }
    }
}
