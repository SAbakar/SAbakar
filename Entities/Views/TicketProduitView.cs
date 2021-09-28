using System;

namespace Entities.Views
{
    public class TicketProduitView
    {
		public int IdTicketProduit { get; set; }
		public int QteCommandeeUnitaire { get; set; }
		public int QteCommandeeKilo { get; set; }
		public int QteLivreeRecueUnitaire { get; set; }
		public int QteLivreeRecueKilo { get; set; }
		public int IdTicket { get; set; }
		/*------------------  Proprietés Ticket ---------------------*/
		public DateTime DateCommande{ get; set; }
		public DateTime DateValidation{ get; set; }
		public DateTime DateLivraison{ get; set; }
		public string ObservationTicket{ get; set; }
		public bool IsTicketValider{ get; set; }
		/*------------------------------------------------------------*/
		public int IdProduit { get; set; }
		/*------------------  Proprietés Produit ---------------------*/
		public string NomProduit{ get; set; }
		public string CodeEAN{ get; set; }
		public decimal PrixAchat{ get; set; }
		public decimal PrixVente{ get; set; }
		public decimal PoidsTotal{ get; set; }
		public int StockMinimalUnite{ get; set; }
		public decimal StockMinimalPoids{ get; set; }
		public int QteReassort{ get; set; }
		public bool IsEnKilogramme{ get; set; }
		public int UVC{ get; set; }
		public int ShortTime{ get; set; }
		public string ImageProduit{ get; set; }
		/*------------------------------------------------------------*/
		    
	}
}
