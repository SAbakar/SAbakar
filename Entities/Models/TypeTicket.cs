using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("TypeTicket")]
    public class TypeTicket
    {
        [Key]
        public int IdTypeTicket { get; set; }
        public string NomTypeTicket { get; set; }
    }
}