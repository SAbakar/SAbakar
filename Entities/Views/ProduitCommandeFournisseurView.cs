using System;

namespace Entities.Views
{
    public class ProduitCommandeFournisseurView
    {
		public int IdProduitCommandeFournisseur { get; set; }
		public int QtePdtCmdeFsseurUnite { get; set; }
		public int QtePdtCmdeFsseurKilo { get; set; }
		public int IdCommandeFournisseur { get; set; }
		/*------------------  Proprietés CommandeFournisseur ---------------------*/
		public string NumeroCmdeFsseur{ get; set; }
		public DateTime DateCmdeFsseur{ get; set; }
		public string ObsCmdeFsseur{ get; set; }
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
