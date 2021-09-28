using System.Threading.Tasks;
using Entities.Views;

namespace Divers.RealTime
{  
    public interface IHubClient  
    {  
        Task BroadcastMessage(string action,
                              int id = 0,
                              string[] elements =null);
        Task TicketPiocher(int idService, int idTypeService);
    }  
}  