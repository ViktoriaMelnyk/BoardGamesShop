
using BoardGames.DataAccess;
using BoardGames.DataAccess.Repository.IRepository;
using BoardGames.Models;
using BoardGames.Models.ViewModels;
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
            return View();
        }
        
        //GET
        public IActionResult Upsert(int? id)
        {
            GameVM gameVM = new()
            {
                Game = new(),
                CategoryList = _db.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
            }
;
            if (id == null || id == 0)
            {
                //create
                return View(gameVM);
                
            }
            else
            {
                //update
                gameVM.Game = _db.Game.GetFirstOrDefault(u => u.Id == id);
                return View(gameVM);

            }
           


        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(GameVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"img\gamesImg");
                    var extension = Path.GetExtension(file.FileName);
                    if (obj.Game.ImageUrl != null)
                    {
                        var oldImgPath = Path.Combine(wwwRootPath,obj.Game.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImgPath))
                        {
                            System.IO.File.Delete(oldImgPath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads,fileName+ extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Game.ImageUrl = @"\img\gamesImg\" + fileName + extension;
                }
                if(obj.Game.Id == 0)
                {
                    _db.Game.Add(obj.Game);
                }
                else
                {
                    _db.Game.Update(obj.Game);
                }
                
                _db.Save();
                TempData["success"] = "Gra dodana pomyślnie";
                return RedirectToAction("Index");

            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var gameList = _db.Game.GetAll(includeProperties:"Category");
            return Json(new { data = gameList });
        }
        //POST
        [HttpDelete]
     
        public IActionResult Delete(int? id)
        {
            var obj = _db.Game.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {

                return Json(new {success = false, message ="Error"});

            }
            var oldImgPath = Path.Combine(_hostEnviroment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImgPath))
            {
                System.IO.File.Delete(oldImgPath);
            }

            _db.Game.Remove(obj);
            _db.Save();
           
            return Json(new { success = true, message = "Usunięto pomyślnie" });


        }
        #endregion
    }

}
