using AgencyMVC.Core.Models;
using AgencyMVC.Core.RepositoryAbstract;
using AgencyMVC.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyMVC.Data.RepositoryConcretes
{
    public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
