using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pojisteni.DataAccess.Repository.IRepository;
using Pojisteni.Models;
using Pojisteni.Models.ViewModels;

namespace Pojisteni.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PojistnikController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PojistnikController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            //Pomocí ViewModelu - zobrazení kategorie-pojistky!
            //Příprava dropdownu pro View
            PojistnikVM pojistnikVM = new PojistnikVM()
            {
                Pojistnik = new Pojistnik(),
                TypyPojisteni = _unitOfWork.Pojistka.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Nazev,      
                    Value = i.PojisteniId.ToString()
                }),   
            };

            if (id == null)
            {
                //CREATE
                return View(pojistnikVM);
            }
            //EDIT
            pojistnikVM.Pojistnik = _unitOfWork.Pojistnik.Get(id.GetValueOrDefault());
            if(pojistnikVM.Pojistnik == null)
            {
                return NotFound();
            }
            return View(pojistnikVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PojistnikVM pojistnikVM)
        {
            if (ModelState.IsValid)
            {
                if(pojistnikVM.Pojistnik.PojisteniId == 0)
                {
                    _unitOfWork.Pojistnik.Add(pojistnikVM.Pojistnik);                   
                }
                else
                {
                    _unitOfWork.Pojistnik.Update(pojistnikVM.Pojistnik);                    
                }
                _unitOfWork.Save();
                TempData["success"] = "Pojistník byl úspěšně vytvořen.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                pojistnikVM.TypyPojisteni = _unitOfWork.Pojistka.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Nazev,
                    Value = i.PojisteniId.ToString()
                });  
                if(pojistnikVM.Pojistnik.PojistnikId != 0)
                {
                    pojistnikVM.Pojistnik = _unitOfWork.Pojistnik.Get(pojistnikVM.Pojistnik.PojistnikId);
                }
            }
            return View(pojistnikVM);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Pojistnik.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Mazání se nezdařilo!" });
            }                        
            _unitOfWork.Pojistnik.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Mazání proběhlo úspěšně." });
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            //nezapomenout INCLUDE PROPERTIES:
            var allObj = _unitOfWork.Pojistnik.GetAll(includeProperties:"Pojistka");
            return Json(new {data = allObj});
        }

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var objFromDb = _unitOfWork.Pojistnik.Get(id);
        //    if(objFromDb == null)
        //    {
        //        return Json(new { success = false, message = "Mazání se nezdařilo" });
        //    }
        //    _unitOfWork.Pojistnik.Remove(objFromDb);
        //    _unitOfWork.Save();
        //    return Json(new { success = true, message = "Pojistník byla úspěšně odstraněna" });
        //}
        #endregion
    }
}