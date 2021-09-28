using System;

namespace Entities.Views
{
    public class TicketView
    {
		public int IdTicket { get; set; }
		public DateTime DateCommande { get; set; }
		public DateTime DateValidation { get; set; }
		public DateTime DateLivraison { get; set; }
		public string ObservationTicket { get; set; }
		public bool IsTicketValider { get; set; }
		public int IdService { get; set; }
		/*------------------  Proprietés Service ---------------------*/
		public string NomService{ get; set; }
		/*------------------------------------------------------------*/
		public int IdClient { get; set; }
		/*------------------  Proprietés Client ---------------------*/
		public string NomClient{ get; set; }
		/*------------------------------------------------------------*/
		public int IdFournisseur { get; set; }
		/*------------------  Proprietés Fournisseur ---------------------*/
		public string NomFournisseur{ get; set; }
		/*------------------------------------------------------------*/
		public int IdPersonnel { get; set; }
		/*------------------  Proprietés Personnel ---------------------*/
		public string NomPersonnel{ get; set; }
		public string PrenomPersonnel{ get; set; }
		public string TelPersonnel{ get; set; }
		/*------------------------------------------------------------*/
		public int IdValideur { get; set; }
		public int IdTypeTicket { get; set; }
		/*------------------  Proprietés TypeTicket ---------------------*/
		public string NomTypeTicket{ get; set; }
		/*------------------------------------------------------------*/
		    
	}
}
