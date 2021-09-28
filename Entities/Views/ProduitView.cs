using System;

namespace Entities.Views
{
    public class ProduitView
    {
		public int IdProduit { get; set; }
		public string NomProduit { get; set; }
		public string CodeEAN { get; set; }
		public decimal PrixAchat { get; set; }
		public decimal PrixVente { get; set; }
		public decimal PoidsTotal { get; set; }
		public int StockMinimalUnite { get; set; }
		public decimal StockMinimalPoids { get; set; }
		public int QteReassort { get; set; }
		public bool IsEnKilogramme { get; set; }
		public int UVC { get; set; }
		public int ShortTime { get; set; }
		public string ImageProduit { get; set; }
		public int IdFamille { get; set; }
		/*------------------  Proprietés Famille ---------------------*/
		public string NomFamille{ get; set; }
		/*------------------------------------------------------------*/
		public int IdSousFamille { get; set; }
		/*------------------  Proprietés SousFamille ---------------------*/
		public string NomSousFamille{ get; set; }
		/*------------------------------------------------------------*/
		public int IdZoneStockage { get; set; }
		/*------------------  Proprietés ZoneStockage ---------------------*/
		public string NomZoneStockage{ get; set; }
		/*------------------------------------------------------------*/
		public int IdSource { get; set; }
		/*------------------  Proprietés Source ---------------------*/
		public string NomSource{ get; set; }
		/*------------------------------------------------------------*/
		public int IdOrigine { get; set; }
		/*------------------  Proprietés Origine ---------------------*/
		public string NomOrigine{ get; set; }
		/*------------------------------------------------------------*/
		public int IdMarque { get; set; }
		/*------------------  Proprietés Marque ---------------------*/
		public string NomMarque{ get; set; }
		/*------------------------------------------------------------*/
		public int IdUnite { get; set; }
		/*------------------  Proprietés Unite ---------------------*/
		public string NomUnite{ get; set; }
		/*------------------------------------------------------------*/
		public int IdUniteFacturation { get; set; }
		public int IdUniteGestionStock { get; set; }
		    
	}
}
