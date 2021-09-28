using System;

namespace Entities.Views
{
    public class FactureView
    {
		public int IdFacture { get; set; }
		public string NumeroFacture { get; set; }
		public DateTime DateFacture { get; set; }
		public decimal RemiseFacture { get; set; }
		public int IdPersonnel { get; set; }
		/*------------------  Proprietés Personnel ---------------------*/
		public string NomPersonnel{ get; set; }
		public string PrenomPersonnel{ get; set; }
		public string TelPersonnel{ get; set; }
		/*------------------------------------------------------------*/
		public int IdTicket { get; set; }
		/*------------------  Proprietés Ticket ---------------------*/
		public DateTime DateCommande{ get; set; }
		public DateTime DateValidation{ get; set; }
		public DateTime DateLivraison{ get; set; }
		public string ObservationTicket{ get; set; }
		public bool IsTicketValider{ get; set; }
		/*------------------------------------------------------------*/
		    
	}
}
