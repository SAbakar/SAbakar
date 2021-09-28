using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Marque")]
    public class Marque
    {
        [Key]
        public int IdMarque { get; set; }
        public string NomMarque { get; set; }
    }
}