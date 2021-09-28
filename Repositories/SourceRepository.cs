using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class SourceRepository : RepositoryBase<Source>,ISourceRepository
    {	
		public SourceRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<SourceView> SOU()
        {
            return from s in AppDBContext.Sources
                select new SourceView()
                {   
                    IdSource = s.IdSource,
                    NomSource = s.NomSource,
                    
                };
        }
        
        public SourceView GetListAllSources(int idSource)
        {
           return SOU().FirstOrDefault(c=>c.IdSource ==idSource);          
        }

        public IEnumerable<SourceView> GetListAllSources()
        {
            return SOU().ToList();
        }
    }
}
	
