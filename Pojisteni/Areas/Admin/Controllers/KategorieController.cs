using Microsoft.AspNetCore.Mvc;
using Pojisteni.DataAccess.Repository.IRepository;
using Pojisteni.Models;

namespace Pojisteni.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KategorieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public KategorieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Kategorie kategorie = new Kategorie();
            if (id == null)
            {
                //CREATE
                return View(kategorie);
            }
            //EDIT
            kategorie = _unitOfWork.Kategorie.Get(id.GetValueOrDefault());
            if(kategorie == null)
            {
                return NotFound();
            }
            return View(kategorie);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Kategorie kategorie)
        {
            if (ModelState.IsValid)
            {
                if(kategorie.KategorieId == 0)
                {
                    _unitOfWork.Kategorie.Add(kategorie);                   
                }
                else
                {
                    _unitOfWork.Kategorie.Update(kategorie);                    
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); //ne Magic String "Index"
            }
            return View(kategorie);
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Kategorie.GetAll();
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Kategorie.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Mazání se nezdařilo" });
            }
            _unitOfWork.Kategorie.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Kategorie byla úspěšně odstraněna" });
        }
        #endregion
    }
}
