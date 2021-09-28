using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface ISourceRepository : IRepositoryBase<Source>
    {
        IEnumerable<SourceView> GetListAllSources();
        SourceView GetListAllSources(int idSource);
    }
}
	
