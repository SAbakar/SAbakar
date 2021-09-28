using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("SousFamille")]
    public class SousFamille
    {
        [Key]
        public int IdSousFamille { get; set; }
        public string NomSousFamille { get; set; }
        public int IdFamille { get; set; }
        [ForeignKey("IdFamille")]
        public virtual Famille Famille { get; set; }
    }
}