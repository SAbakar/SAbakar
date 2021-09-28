using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("SubstitutionProduit")]
    public class SubstitutionProduit
    {
        [Key]
        public int IdSubstitutionProduit { get; set; }
        public int IdProduitPrincipal { get; set; }        
        public int IdProduitSubstitution { get; set; }
        
    }
}