using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("ProduitBCI")]
    public class ProduitBCI
    {
        [Key]
        public int IdProduitBCI { get; set; }
        public int QteCommandeUnite { get; set; }
        public int QteCommandeKilo { get; set; }
        public int IdProduit { get; set; }
        [ForeignKey("IdProduit")]
        public virtual Produit Produit { get; set; }

        public int IdBCI { get; set; }
        [ForeignKey("IdBCI")]
        public virtual BCI BCI { get; set; }
    }
}