using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Origine")]
    public class Origine
    {
        [Key]
        public int IdOrigine { get; set; }
        public string NomOrigine { get; set; }
    }
}