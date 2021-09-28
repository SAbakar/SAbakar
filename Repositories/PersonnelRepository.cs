using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class PersonnelRepository : RepositoryBase<Personnel>,IPersonnelRepository
    {	
		public PersonnelRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<PersonnelView> PER()
        {
            return from p in AppDBContext.Personnels
                select new PersonnelView()
                {   
                    IdPersonnel = p.IdPersonnel,
                    NomPersonnel = p.NomPersonnel,
                    PrenomPersonnel = p.PrenomPersonnel,
                    TelPersonnel = p.TelPersonnel,
                    IdFonction = p.IdFonction,
                    NomFonction = p.Fonction.NomFonction,
                    IdService = p.IdService,
                    NomService = p.Service.NomService,
                    
                };
        }
        
        public PersonnelView GetListAllPersonnels(int idPersonnel)
        {
           return PER().FirstOrDefault(c=>c.IdPersonnel ==idPersonnel);          
        }

        public IEnumerable<PersonnelView> GetListAllPersonnels()
        {
            return PER().ToList();
        }
    }
}
	
