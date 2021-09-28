using System;

namespace Entities.Views
{
    public class StockProduitView
    {
		public int IdStockProduit { get; set; }
		public int QteInitialStockUnite { get; set; }
		public int QteInitialStockKilo { get; set; }
		public int QteStockUnite { get; set; }
		public int QteStockKilo { get; set; }
		public DateTime DLC { get; set; }
		public int LotParDLC { get; set; }
		public DateTime DateMajStock { get; set; }
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
