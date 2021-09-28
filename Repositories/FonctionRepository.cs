using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class FonctionRepository : RepositoryBase<Fonction>,IFonctionRepository
    {	
		public FonctionRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<FonctionView> FON()
        {
            return from f in AppDBContext.Fonctions
                select new FonctionView()
                {   
                    IdFonction = f.IdFonction,
                    NomFonction = f.NomFonction,
                    
                };
        }
        
        public FonctionView GetListAllFonctions(int idFonction)
        {
           return FON().FirstOrDefault(c=>c.IdFonction ==idFonction);          
        }

        public IEnumerable<FonctionView> GetListAllFonctions()
        {
            return FON().ToList();
        }
    }
}
	
