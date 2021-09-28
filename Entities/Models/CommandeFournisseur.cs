using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("CommandeFournisseur")]
    public class CommandeFournisseur
    {
        [Key]
        public int IdCommandeFournisseur { get; set; }
        public string  NumeroCmdeFsseur { get; set; }
        public DateTime DateCmdeFsseur { get; set; }
        public string  ObsCmdeFsseur { get; set; }
        public int IdFournisseur { get; set; }
        [ForeignKey("IdFournisseur")]
        public virtual Fournisseur Fournisseur { get; set; }
        
    }
}