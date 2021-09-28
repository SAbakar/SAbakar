using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("TypeBCI")]
    public class TypeBCI
    {
        [Key]
        public int IdTypeBCI { get; set; }
        public string NomTypeBCI { get; set; }
    }
}