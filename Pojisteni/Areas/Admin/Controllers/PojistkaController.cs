using Microsoft.AspNetCore.Mvc;
using Pojisteni.DataAccess.Repository.IRepository;
using Pojisteni.Models;

namespace Pojisteni.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PojistkaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PojistkaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Pojistka pojistka = new Pojistka();
            if (id == null)
            {
                //CREATE
                return View(pojistka);
            }
            //EDIT
            pojistka = _unitOfWork.Pojistka.Get(id.GetValueOrDefault());
            if(pojistka == null)
            {
                return NotFound();
            }
            return View(pojistka);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Pojistka pojistka)
        {
            if (ModelState.IsValid)
            {
                if(pojistka.PojisteniId == 0)
                {
                    _unitOfWork.Pojistka.Add(pojistka);                   
                }
                else
                {
                    _unitOfWork.Pojistka.Update(pojistka);                    
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); //ne Magic String "Index"
            }
            return View(pojistka);
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Pojistka.GetAll();
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Pojistka.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Mazání se nezdařilo" });
            }
            _unitOfWork.Pojistka.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Pojištění bylo úspěšně odstraněno" });
        }
        #endregion
    }
}
