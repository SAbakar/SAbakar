using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class ZoneStockageRepository : RepositoryBase<ZoneStockage>,IZoneStockageRepository
    {	
		public ZoneStockageRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<ZoneStockageView> ZON()
        {
            return from z in AppDBContext.ZoneStockages
                select new ZoneStockageView()
                {   
                    IdZoneStockage = z.IdZoneStockage,
                    NomZoneStockage = z.NomZoneStockage,
                    
                };
        }
        
        public ZoneStockageView GetListAllZoneStockages(int idZoneStockage)
        {
           return ZON().FirstOrDefault(c=>c.IdZoneStockage ==idZoneStockage);          
        }

        public IEnumerable<ZoneStockageView> GetListAllZoneStockages()
        {
            return ZON().ToList();
        }
    }
}
	
