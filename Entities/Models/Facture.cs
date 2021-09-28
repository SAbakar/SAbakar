using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Facture")]
    public class Facture
    {
        [Key]
        public int IdFacture { get; set; }
        public string NumeroFacture { get; set; }
        public DateTime DateFacture { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal RemiseFacture { get; set; }
        public int IdPersonnel { get; set; } //Prepapre par
        [ForeignKey("IdPersonnel")]
        public virtual Personnel Personnel { get; set; }
        public int IdTicket { get; set; }
        [ForeignKey("IdTicket")]
        public virtual Ticket Ticket { get; set; }
    }
}