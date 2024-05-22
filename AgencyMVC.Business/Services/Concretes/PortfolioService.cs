using AgencyMVC.Business.Exceptions;
using AgencyMVC.Business.Services.Abstract;
using AgencyMVC.Core.Models;
using AgencyMVC.Core.RepositoryAbstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyMVC.Business.Services.Concretes
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PortfolioService(IPortfolioRepository portfolioRepository,IWebHostEnvironment webHostEnvironment)
        {
            _portfolioRepository = portfolioRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public void AddPortfolio(Portfolio portfolio)
        {
            if(portfolio == null) throw new EntityNullReferenceException("","portfolio null");
            if (portfolio.ImageFile == null) throw new Exceptions.FileNotFoundException("ImageFile","file image");
            if (!portfolio.ImageFile.ContentType.Contains("image/")) throw new FileContentypeException("ImageFile","image file!!");
            if (portfolio.ImageFile.Length > 2097152) throw new FileSizeException("ImageFile","Size error!!");

            string filename = Guid.NewGuid().ToString() + Path.GetExtension(portfolio.ImageFile.FileName);
            string path = _webHostEnvironment.WebRootPath +@"\uploads\portfolios\" + filename;

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                portfolio.ImageFile.CopyTo(stream);
            }

            portfolio.ImageUrl = filename;
            _portfolioRepository.Add(portfolio);
            _portfolioRepository.Commit();
        }

        public void DeletePortfolio(int id)
        {
            var exsitsPortfolio = _portfolioRepository.Get(x=>x.Id == id);
            if (exsitsPortfolio == null) throw new EntityNullReferenceException("","portfolio nullls");
            string path = _webHostEnvironment.WebRootPath +@"\uploads\portfolios\" + exsitsPortfolio.ImageUrl;
            if (!File.Exists(path)) throw new Exceptions.FileNotFoundException("ImageFile", "file image!!");
            {
                File.Delete(path);
            }
            _portfolioRepository.Delete(exsitsPortfolio);
            _portfolioRepository.Commit();
        }

        public List<Portfolio> GetAllPortfolio(Func<Portfolio, bool>? func = null)
        {
            return _portfolioRepository.GetAll(func);
        }

        public Portfolio GetPortfolio(Func<Portfolio, bool>? func = null)
        {
           return _portfolioRepository.Get(func);
        }

        public void UpdatePortfolio(int id, Portfolio portfolio)
        {
            var exsitsPortfolio = _portfolioRepository.Get(x => x.Id == id);
            if (exsitsPortfolio == null) throw new EntityNullReferenceException("", "portfolio null");

            if (portfolio == null) throw new EntityNullReferenceException("", "portfolio null");
            if (portfolio.ImageFile != null)
            {
                if (!portfolio.ImageFile.ContentType.Contains("image/")) throw new FileContentypeException("ImageFile", "image file!!");
                if (portfolio.ImageFile.Length > 2097152) throw new FileSizeException("ImageFile", "Size error!!");

                string oldpath = _webHostEnvironment.WebRootPath + @"\uploads\portfolios\" + exsitsPortfolio.ImageUrl;
                if (!File.Exists(oldpath)) throw new Exceptions.FileNotFoundException("ImageFile", "File not found!");
                {
                    File.Delete(oldpath);
                }
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(portfolio.ImageFile.FileName);
                string path = _webHostEnvironment.WebRootPath + @"\uploads\portfolios\" + filename;

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    portfolio.ImageFile.CopyTo(stream);
                }
                exsitsPortfolio.ImageUrl = filename;
            }
            exsitsPortfolio.Title = portfolio.Title;
            exsitsPortfolio.Description = portfolio.Description;
            _portfolioRepository.Commit();
        }
    }
}
