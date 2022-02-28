
using BoardGames.DataAccess;
using BoardGames.DataAccess.Repository.IRepository;
using BoardGames.Models;
using BoardGames.Models.ViewModels;
using BoardGamesShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.Controllers
{[Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _db;




        public CompanyController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        //GET
        public IActionResult Upsert(int? id)
        {
            Company company = new()

;
            if (id == null || id == 0)
            {
                //create
                return View(company);
                
            }
            else
            {
                //update
                company = _db.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);

            }
           


        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                
                if(obj.Id == 0)
                {
                    _db.Company.Add(obj);
                    TempData["success"] = "Firma dodana pomyślnie";
                }
                else
                {
                    _db.Company.Update(obj);
                    TempData["success"] = "Firma edytowana pomyślnie";
                }
                
                _db.Save();
                
                return RedirectToAction("Index");

            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _db.Company.GetAll();
            return Json(new { data = companyList });
        }
        //POST
        [HttpDelete]
     
        public IActionResult Delete(int? id)
        {
            var obj = _db.Company.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {

                return Json(new {success = false, message ="Error"});

            }
            

            _db.Company.Remove(obj);
            _db.Save();
           
            return Json(new { success = true, message = "Usunięto pomyślnie" });


        }
        #endregion
    }

}
