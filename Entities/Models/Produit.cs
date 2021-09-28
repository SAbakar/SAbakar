using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Produit")]
    public class Produit
    {
        [Key]
        public int IdProduit { get; set; }
        public string NomProduit { get; set; }
        public string CodeEAN { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal PrixAchat { get; set; }
        
        [Column(TypeName = "decimal(18,0)")]
        public decimal PrixVente { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal PoidsTotal { get; set; }
        
        public int StockMinimalUnite { get; set; }
        
        [Column(TypeName = "decimal(18,0)")]
        public decimal StockMinimalPoids { get; set; }
        public int QteReassort { get; set; }
        public bool IsEnKilogramme { get; set; }
        public int UVC { get; set; } //Unite de vente conditionner
        public int ShortTime { get; set; } //Nombre de jour avant la DLC pour envoyer l'alerte
        public string ImageProduit { get; set; }
        public int IdFamille { get; set; }
        [ForeignKey("IdFamille")]
        public virtual Famille Famille { get; set; }
        public int IdSousFamille { get; set; }
        [ForeignKey("IdSousFamille")]
        public virtual SousFamille SousFamille { get; set; }

        public int IdZoneStockage{ get; set; }
        [ForeignKey("IdZoneStockage")]
        public virtual ZoneStockage ZoneStockage { get; set; } 

        public int IdSource { get; set; }
        [ForeignKey("IdSource")]
        public virtual Source Source { get; set; }

        public int IdOrigine { get; set; }
        
        public int IdMarque { get; set; }       

        public int IdUnite { get; set; }
        [ForeignKey("IdUnite")]
        public virtual Unite Unite { get; set; }

        public int IdUniteFacturation { get; set; }
        public int IdUniteGestionStock { get; set; }


    }
}