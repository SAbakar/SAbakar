using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class SousFamilleRepository : RepositoryBase<SousFamille>,ISousFamilleRepository
    {	
		public SousFamilleRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<SousFamilleView> SOU()
        {
            return from s in AppDBContext.SousFamilles
                select new SousFamilleView()
                {   
                    IdSousFamille = s.IdSousFamille,
                    NomSousFamille = s.NomSousFamille,
                    IdFamille = s.IdFamille,
                    NomFamille = s.Famille.NomFamille,
                    
                };
        }
        
        public SousFamilleView GetListAllSousFamilles(int idSousFamille)
        {
           return SOU().FirstOrDefault(c=>c.IdSousFamille ==idSousFamille);          
        }

        public IEnumerable<SousFamilleView> GetListAllSousFamilles()
        {
            return SOU().ToList();
        }
    }
}
	
