using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;  
  
namespace Divers.RealTime
{  
    public class FromApiBroadcastHub : Hub<IHubClient>  
    {  

    }  
    public class ToApiBroadCastHub : Hub
    {
        public Task PiocherTicket(int idService, int idTypeService) {
            return Clients.Others.SendAsync("piocherTicket", idService,idTypeService); 
        } 
    }
}