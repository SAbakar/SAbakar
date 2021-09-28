using System;

namespace Entities.Views
{
    public class CatalogueProduitView
    {
		public int IdCatalogueProduit { get; set; }
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
		public int IdCatalogue { get; set; }
		/*------------------  Proprietés Catalogue ---------------------*/
		public string NomCatalogue{ get; set; }
		/*------------------------------------------------------------*/
		    
	}
}
