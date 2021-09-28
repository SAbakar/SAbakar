using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IZoneStockageRepository : IRepositoryBase<ZoneStockage>
    {
        IEnumerable<ZoneStockageView> GetListAllZoneStockages();
        ZoneStockageView GetListAllZoneStockages(int idZoneStockage);
    }
}
	
