using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class FournisseurRepository : RepositoryBase<Fournisseur>,IFournisseurRepository
    {	
		public FournisseurRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<FournisseurView> FOU()
        {
            return from f in AppDBContext.Fournisseurs
                select new FournisseurView()
                {   
                    IdFournisseur = f.IdFournisseur,
                    NomFournisseur = f.NomFournisseur,
                    IdSource = f.IdSource,
                    NomSource = f.Source.NomSource,
                    
                };
        }
        
        public FournisseurView GetListAllFournisseurs(int idFournisseur)
        {
           return FOU().FirstOrDefault(c=>c.IdFournisseur ==idFournisseur);          
        }

        public IEnumerable<FournisseurView> GetListAllFournisseurs()
        {
            return FOU().ToList();
        }
    }
}
	
