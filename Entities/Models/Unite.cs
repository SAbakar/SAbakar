using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Unite")]
    public class Unite
    {
        [Key]
        public int IdUnite { get; set; }
        public string NomUnite { get; set; }
    }
}