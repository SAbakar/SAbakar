using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("StockProduit")]
    public class StockProduit
    {
        [Key]
        public int IdStockProduit { get; set; }
        public int QteInitialStockUnite { get; set; }
        public int QteInitialStockKilo { get; set; }
        public int QteStockUnite { get; set; }
        public int QteStockKilo { get; set; }
        public DateTime DLC { get; set; } //Date limite de consommation
        public int LotParDLC { get; set; }
        public DateTime DateMajStock { get; set; }
        public int IdProduit { get; set; }
        [ForeignKey("IdProduit")]
        public virtual Produit Produit { get; set; }
    }
}