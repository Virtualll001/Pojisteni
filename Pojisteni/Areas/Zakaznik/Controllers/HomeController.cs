using Microsoft.AspNetCore.Mvc;
using Pojisteni.DataAccess.Repository.IRepository;
using Pojisteni.Models;
using Pojisteni.Models.ViewModels;
using System.Diagnostics;

namespace Pojisteni.Areas.Zakaznik.Controllers
{
    [Area("Zakaznik")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Pojistka> pojisteniSeznam = _unitOfWork.Pojistka.GetAll(includeProperties: "Kategorie");
            return View(pojisteniSeznam);
        }

        public IActionResult Projekt()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}