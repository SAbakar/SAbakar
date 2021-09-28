using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("ProduitCommandeFournisseur")]
    public class ProduitCommandeFournisseur
    {
        [Key]
        public int IdProduitCommandeFournisseur { get; set; }
        public int QtePdtCmdeFsseurUnite { get; set; }
        public int QtePdtCmdeFsseurKilo { get; set; }
        public int IdCommandeFournisseur { get; set; }
        [ForeignKey("IdCommandeFournisseur")]
        public virtual CommandeFournisseur CommandeFournisseur { get; set; }
        public int IdProduit { get; set; }
        [ForeignKey("IdProduit")]
        public virtual Produit Produit { get; set; }
    }
}