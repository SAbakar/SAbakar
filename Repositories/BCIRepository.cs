using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class BCIRepository : RepositoryBase<BCI>,IBCIRepository
    {	
		public BCIRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<BCIView> BCI()
        {
            return from b in AppDBContext.BCIs
                select new BCIView()
                {   
                    IdBCI = b.IdBCI,
                    NumeroBCI = b.NumeroBCI,
                    DateBCI = b.DateBCI,
                    DateValidationBCI = b.DateValidationBCI,
                    IsValider = b.IsValider,
                    IsAnnuler = b.IsAnnuler,
                    ObsBCI = b.ObsBCI,
                    IdPersonnel = b.IdPersonnel,
                    NomPersonnel = b.Personnel.NomPersonnel,
                    PrenomPersonnel = b.Personnel.PrenomPersonnel,
                    TelPersonnel = b.Personnel.TelPersonnel,
                    IdTypeBCI = b.IdTypeBCI,
                    NomTypeBCI = b.TypeBCI.NomTypeBCI,
                    
                };
        }
        
        public BCIView GetListAllBCIs(int idBCI)
        {
           return BCI().FirstOrDefault(c=>c.IdBCI ==idBCI);          
        }

        public IEnumerable<BCIView> GetListAllBCIs()
        {
            return BCI().ToList();
        }
    }
}
	
