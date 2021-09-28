using System;

namespace Entities.Views
{
    public class ProduitBCIView
    {
		public int IdProduitBCI { get; set; }
		public int QteCommandeUnite { get; set; }
		public int QteCommandeKilo { get; set; }
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
		public int IdBCI { get; set; }
		/*------------------  Proprietés BCI ---------------------*/
		public string NumeroBCI{ get; set; }
		public DateTime DateBCI{ get; set; }
		public DateTime DateValidationBCI{ get; set; }
		public bool IsValider{ get; set; }
		public bool IsAnnuler{ get; set; }
		public string ObsBCI{ get; set; }
		/*------------------------------------------------------------*/
		    
	}
}
