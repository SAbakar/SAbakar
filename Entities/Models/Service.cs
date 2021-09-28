using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Service")]
    public class Service
    {
        [Key]
        public int IdService { get; set; }
        public string NomService { get; set; }
    }
}