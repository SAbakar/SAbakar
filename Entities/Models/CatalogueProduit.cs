using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("CatalogueProduit")]
    public class CatalogueProduit
    {
        [Key]
        public int IdCatalogueProduit { get; set; }
        public int IdProduit { get; set; }
        [ForeignKey("IdProduit")]
        public virtual Produit Produit { get; set; }

        public int IdCatalogue { get; set; }
        [ForeignKey("IdCatalogue")]
        public virtual Catalogue Catalogue { get; set; }
    }
}