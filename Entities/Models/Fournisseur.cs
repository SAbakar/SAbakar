using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Fournisseur")]
    public class Fournisseur
    {
        [Key]
        public int IdFournisseur { get; set; }
        public string NomFournisseur { get; set; }
        public int IdSource { get; set; }
        [ForeignKey("IdSource")]
        public virtual Source Source { get; set; }
    }
}