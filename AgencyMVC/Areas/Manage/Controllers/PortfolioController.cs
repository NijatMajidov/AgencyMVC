using AgencyMVC.Business.Exceptions;
using AgencyMVC.Business.Services.Abstract;
using AgencyMVC.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgencyMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }
        public IActionResult Index()
        {
            return View(_portfolioService.GetAllPortfolio());
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Portfolio portfolio)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _portfolioService.AddPortfolio(portfolio);
            }
            catch(EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch(Business.Exceptions.FileNotFoundException ex)
            {

                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch(FileContentypeException ex)
            {

                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {

                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View();
            }
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var exsistPortfolio = _portfolioService.GetPortfolio(x=>x.Id == id);
            if (exsistPortfolio == null) return View("Error");
            return View(exsistPortfolio);
        }

        public IActionResult DeletePortfolio(int id)
        {
            var exsistPortfolio = _portfolioService.GetPortfolio(x => x.Id == id);
            if (exsistPortfolio == null) return View("Error");

            try
            {
                _portfolioService.DeletePortfolio(exsistPortfolio.Id);
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {

                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction("index");
        }

        public IActionResult Update(int id)
        {
            var exsistPortfolio = _portfolioService.GetPortfolio(x => x.Id == id);
            if (exsistPortfolio == null) return View("Error");
            return View(exsistPortfolio);
        }
        [HttpPost]
        public IActionResult Update(int id, Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _portfolioService.UpdatePortfolio(portfolio.Id,portfolio);
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {

                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (FileContentypeException ex)
            {

                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {

                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
