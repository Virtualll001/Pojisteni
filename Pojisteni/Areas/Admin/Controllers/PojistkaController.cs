using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pojisteni.DataAccess.Repository.IRepository;
using Pojisteni.Models;
using Pojisteni.Models.ViewModels;
using Pojisteni.Utility;

namespace Pojisteni.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PojistkaController : Controller
    {
        //Stored Procedures - verze v poznámkách - (viz migrace)       
        private readonly IUnitOfWork _unitOfWork;

        //kvůli obrázku je nutný HostEnviroment!
        private readonly IWebHostEnvironment _hostEnvironment;
        public PojistkaController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            PojistkaVM pojistkaVM = new PojistkaVM()
            {
                Pojistka = new Pojistka(),
                TypSeznam = _unitOfWork.Kategorie.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Typ,
                    Value = i.KategorieId.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return View(pojistkaVM);
            }
            else
            {
                pojistkaVM.Pojistka = _unitOfWork.Pojistka.GetFirstOrDefault(u => u.PojisteniId == id);
                //pojistkaVM.Pojistka = _unitOfWork.Pojistka.Get(id.GetValueOrDefault());
                if (pojistkaVM.Pojistka == null)
                {
                    return NotFound();
                }
                return View(pojistkaVM);
            }

            //var parameter = new DynamicParameters();
            //parameter.Add("@PojisteniId", id);           
            //pojistka = _unitOfWork.SP_Call.OneRecord<Pojistka>(SD.Proc_Pojistka_Get, parameter);
            //pojistka = _unitOfWork.Pojistka.Get(id.GetValueOrDefault());            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PojistkaVM pojistkaVM, IFormFile? file) //UPSERT s IMG!!! == musí být VM a file v parametru
        {
            if (ModelState.IsValid) //TODO rerror: TypSeznam, ImageUrl a Kategorie => invalid
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                //var files = HttpContext.Request.Form.Files;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"img\pojisteni"); //? poslední lomítko???
                    var extension = Path.GetExtension(file.FileName);

                    if (pojistkaVM.Pojistka.ImageUrl != null)
                    {
                        //EDIT = odstranit starý img                     
                        var oldImagePath = Path.Combine(wwwRootPath, pojistkaVM.Pojistka.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(filesStreams);
                    }
                    pojistkaVM.Pojistka.ImageUrl = @"\img\pojisteni\" + fileName + extension;
                }
                else //update bez nového img
                {
                    if (pojistkaVM.Pojistka.PojisteniId != 0)
                    {
                        Pojistka objFromDb = _unitOfWork.Pojistka.Get(pojistkaVM.Pojistka.PojisteniId);
                        pojistkaVM.Pojistka.ImageUrl = objFromDb.ImageUrl;
                    }
                }

                if (pojistkaVM.Pojistka.PojisteniId == 0)
                {
                    _unitOfWork.Pojistka.Add(pojistkaVM.Pojistka);
                }
                else
                {
                    _unitOfWork.Pojistka.Update(pojistkaVM.Pojistka);
                }
                _unitOfWork.Save();
                TempData["success"] = "Pojištění úspěšně uloženo.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                pojistkaVM.TypSeznam = _unitOfWork.Pojistka.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Nazev,
                    Value = i.PojisteniId.ToString()
                });
                if (pojistkaVM.Pojistka.PojisteniId != 0)
                {
                    pojistkaVM.Pojistka = _unitOfWork.Pojistka.Get(pojistkaVM.Pojistka.PojisteniId);
                }
            }
            return View(pojistkaVM);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Pojistka.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Mazání se nezdařilo!" });
            }

            string wwwRootPath = _hostEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(wwwRootPath, objFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Pojistka.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Mazání proběhlo úspěšně." });
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Pojistka.GetAll(includeProperties: "Kategorie");
            //var allObj = _unitOfWork.SP_Call.List<Pojistka>(SD.Proc_Pojistka_GetAll,null);
            return Json(new { data = allObj });
        }

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var parameter = new DynamicParameters();
        //    parameter.Add("@PojisteniId", id);
        //    var objFromDb = _unitOfWork.Pojistka.Get(id);
        //    //var objFromDb = _unitOfWork.SP_Call.OneRecord<Pojistka>(SD.Proc_Pojistka_Get, parameter);
        //    if (objFromDb == null)
        //    {
        //        return Json(new { success = false, message = "Mazání se nezdařilo" });
        //    }
        //    _unitOfWork.Pojistka.Remove(objFromDb);
        //    //_unitOfWork.SP_Call.Execute(SD.Proc_Pojistka_Delete, parameter);
        //    _unitOfWork.Save();
        //    return Json(new { success = true, message = "Pojištění bylo úspěšně odstraněno" });
        //}
        #endregion
    }
}
