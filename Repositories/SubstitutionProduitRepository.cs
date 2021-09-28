using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class SubstitutionProduitRepository : RepositoryBase<SubstitutionProduit>,ISubstitutionProduitRepository
    {	
		public SubstitutionProduitRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<SubstitutionProduitView> SUB()
        {
            return from s in AppDBContext.SubstitutionProduits
                select new SubstitutionProduitView()
                {   
                    IdSubstitutionProduit = s.IdSubstitutionProduit,
                    IdProduitPrincipal = s.IdProduitPrincipal,
                    IdProduitSubstitution = s.IdProduitSubstitution,
                    
                };
        }
        
        public SubstitutionProduitView GetListAllSubstitutionProduits(int idSubstitutionProduit)
        {
           return SUB().FirstOrDefault(c=>c.IdSubstitutionProduit ==idSubstitutionProduit);          
        }

        public IEnumerable<SubstitutionProduitView> GetListAllSubstitutionProduits()
        {
            return SUB().ToList();
        }
    }
}
	
