using Dapper;
using Microsoft.AspNetCore.Mvc;
using Pojisteni.DataAccess.Repository.IRepository;
using Pojisteni.Models;
using Pojisteni.Utility;

namespace Pojisteni.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PojistkaController : Controller
    {
        //upravené na Stored Procedures! (viz migrace)
        //v poznámkách je verze bez SP
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
                return View(pojistka);
            }

            var parameter = new DynamicParameters();
            parameter.Add("@PojisteniId", id);           
            pojistka = _unitOfWork.SP_Call.OneRecord<Pojistka>(SD.Proc_Pojistka_Get, parameter);
            //pojistka = _unitOfWork.Pojistka.Get(id.GetValueOrDefault());
            if (pojistka == null)
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
                var parameter = new DynamicParameters();
                parameter.Add("@Podminky", pojistka.Podminky);

                if (pojistka.PojisteniId == 0)
                {
                    //_unitOfWork.Pojistka.Add(pojistka);
                    _unitOfWork.SP_Call.Execute(SD.Proc_Pojistka_Create, parameter);

                }
                else
                {
                    parameter.Add("@PojisteniId", pojistka.PojisteniId);
                    _unitOfWork.SP_Call.Execute(SD.Proc_Pojistka_Update, parameter);
                    //_unitOfWork.Pojistka.Update(pojistka);                    
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(pojistka);
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            //var allObj = _unitOfWork.Pojistka.GetAll();   
            var allObj = _unitOfWork.SP_Call.List<Pojistka>(SD.Proc_Pojistka_GetAll,null);
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@PojisteniId", id);
            //var objFromDb = _unitOfWork.Pojistka.Get(id);
            var objFromDb = _unitOfWork.SP_Call.OneRecord<Pojistka>(SD.Proc_Pojistka_Get, parameter);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Mazání se nezdařilo" });
            }
            //_unitOfWork.Pojistka.Remove(objFromDb);
            _unitOfWork.SP_Call.Execute(SD.Proc_Pojistka_Delete, parameter);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Pojištění bylo úspěšně odstraněno" });
        }
        #endregion
    }
}
