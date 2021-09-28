using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Personnel")]
    public class Personnel
    {
        [Key]
        public int IdPersonnel { get; set; }
        public string NomPersonnel { get; set; }
        public string PrenomPersonnel { get; set; }
        public string  TelPersonnel { get; set; }
        public int IdFonction { get; set; }
        [ForeignKey("IdFonction")]
        public virtual Fonction Fonction { get; set; }

        public int IdService { get; set; }
        [ForeignKey("IdService")]
        public virtual Service Service { get; set; }
    }
}