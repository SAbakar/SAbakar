using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("ZoneStockage")]
    public class ZoneStockage
    {
        [Key]
        public int IdZoneStockage { get; set; }
        public string NomZoneStockage { get; set; }
    }
}