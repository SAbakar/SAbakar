using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Catalogue")]
    public class Catalogue
    {
        [Key]
        public int IdCatalogue { get; set; }
        public string NomCatalogue { get; set; }
        public int IdClientService { get; set; } //Id Client ou Service
        public int IdTypeCatalogue { get; set; }
        [ForeignKey("IdTypeCatalogue")]
        public virtual TypeCatalogue TypeCatalogue { get; set; }
    }
}