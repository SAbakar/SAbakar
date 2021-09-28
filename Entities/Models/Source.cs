using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Source")]
    public class Source
    {
        [Key]
        public int IdSource { get; set; }
        public string NomSource { get; set; }
    }
}