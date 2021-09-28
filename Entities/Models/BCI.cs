using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("BCI")]
    public class BCI
    {
        [Key]
        public int IdBCI { get; set; }
        public string NumeroBCI { get; set; }
        public DateTime DateBCI { get; set; }
        public DateTime DateValidationBCI { get; set; }
        public bool IsValider { get; set; }
        public bool IsAnnuler { get; set; }
        public string ObsBCI { get; set; }
        public int IdPersonnel { get; set; } //Le demandeur (initiateur du BCI)
        [ForeignKey("IdPersonnel")]
        public virtual Personnel Personnel { get; set; }
        public int IdTypeBCI { get; set; }
        [ForeignKey("IdTypeBCI")]
        public virtual TypeBCI TypeBCI { get; set; }
    }
}