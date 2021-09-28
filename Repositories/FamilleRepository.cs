using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class FamilleRepository : RepositoryBase<Famille>,IFamilleRepository
    {	
		public FamilleRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<FamilleView> FAM()
        {
            return from f in AppDBContext.Familles
                select new FamilleView()
                {   
                    IdFamille = f.IdFamille,
                    NomFamille = f.NomFamille,
                    
                };
        }
        
        public FamilleView GetListAllFamilles(int idFamille)
        {
           return FAM().FirstOrDefault(c=>c.IdFamille ==idFamille);          
        }

        public IEnumerable<FamilleView> GetListAllFamilles()
        {
            return FAM().ToList();
        }
    }
}
	
