using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("TicketProduit")]
    public class TicketProduit
    {
        [Key]
        public int IdTicketProduit { get; set; }
        public int QteCommandeeUnitaire { get; set; }
        public int QteCommandeeKilo { get; set; }
        public int QteLivreeRecueUnitaire { get; set; }
        public int QteLivreeRecueKilo { get; set; }
        public int IdTicket { get; set; }
        [ForeignKey("IdTicket")]
        public virtual Ticket Ticket { get; set; }

        public int IdProduit { get; set; }
        [ForeignKey("IdProduit")]
        public virtual Produit Produit { get; set; }
    }
}