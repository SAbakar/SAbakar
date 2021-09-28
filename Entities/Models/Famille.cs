using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Famille")]
    public class Famille
    {
        [Key]
        public int IdFamille { get; set; }
        public string NomFamille { get; set; }
    }
}