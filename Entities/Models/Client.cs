using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
   [Table("Client")]
   public class Client
   {
      [Key]
      public int IdClient { get; set; }
      public string NomClient { get; set; }
   }
}