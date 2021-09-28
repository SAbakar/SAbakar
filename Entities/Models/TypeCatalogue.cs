using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("TypeCatalogue")]
    public class TypeCatalogue
    {
        [Key]
        public int IdTypeCatalogue { get; set; }
        public string NomTypeCatalogue { get; set; }
    }
}