using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Fonction")]
    public class Fonction
    {
        [Key]
        public int IdFonction { get; set; }
        public string NomFonction { get; set; }
    }
}