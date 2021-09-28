using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class CommandeFournisseurRepository : RepositoryBase<CommandeFournisseur>,ICommandeFournisseurRepository
    {	
		public CommandeFournisseurRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<CommandeFournisseurView> COM()
        {
            return from c in AppDBContext.CommandeFournisseurs
                select new CommandeFournisseurView()
                {   
                    IdCommandeFournisseur = c.IdCommandeFournisseur,
                    NumeroCmdeFsseur = c.NumeroCmdeFsseur,
                    DateCmdeFsseur = c.DateCmdeFsseur,
                    ObsCmdeFsseur = c.ObsCmdeFsseur,
                    IdFournisseur = c.IdFournisseur,
                    NomFournisseur = c.Fournisseur.NomFournisseur,
                    
                };
        }
        
        public CommandeFournisseurView GetListAllCommandeFournisseurs(int idCommandeFournisseur)
        {
           return COM().FirstOrDefault(c=>c.IdCommandeFournisseur ==idCommandeFournisseur);          
        }

        public IEnumerable<CommandeFournisseurView> GetListAllCommandeFournisseurs()
        {
            return COM().ToList();
        }
    }
}
	
