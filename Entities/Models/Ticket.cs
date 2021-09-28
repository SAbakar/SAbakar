using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        [Key]
        public int IdTicket { get; set; }
        public DateTime DateCommande { get; set; }        
        public DateTime DateValidation { get; set; }
        public DateTime DateLivraison { get; set; }
        public string ObservationTicket { get; set; }
        public bool IsTicketValider { get; set; }
        public int IdService { get; set; }
        public int IdClient { get; set; }
        public int IdFournisseur { get; set; }
        public int IdPersonnel { get; set; } //IdPersonne qui a lancer la commande ou receptionner l'arrivage
        [ForeignKey("IdPersonnel")]
        public virtual Personnel Personnel { get; set; }
        public int IdValideur { get; set; }  //IdPersonne qui a valider la commande ou receptionner l'arrivage
        public int IdTypeTicket { get; set; }
        [ForeignKey("IdTypeTicket")]
        public virtual TypeTicket TypeTicket { get; set; }

    }
}