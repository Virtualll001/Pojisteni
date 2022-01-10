using Microsoft.AspNetCore.Mvc;
using Pojisteni.DataAccess.Repository.IRepository;
using Pojisteni.Models;

namespace Pojisteni.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id == null)
            {
                //CREATE
                return View(company);
            }
            //EDIT
            company = _unitOfWork.Company.Get(id.GetValueOrDefault());
            if(company == null)
            {
                return NotFound();
            }
            return View(company);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if(company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);                   
                }
                else
                {
                    _unitOfWork.Company.Update(company);                    
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); //ne Magic String "Index"
            }
            return View(company);
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Company.GetAll();
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Company.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Mazání se nezdařilo" });
            }
            _unitOfWork.Company.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Společnost byla úspěšně odstraněna" });
        }
        #endregion
    }
}
