using System;

namespace Entities.Views
{
    public class CommandeFournisseurView
    {
		public int IdCommandeFournisseur { get; set; }
		public string NumeroCmdeFsseur { get; set; }
		public DateTime DateCmdeFsseur { get; set; }
		public string ObsCmdeFsseur { get; set; }
		public int IdFournisseur { get; set; }
		/*------------------  Proprietés Fournisseur ---------------------*/
		public string NomFournisseur{ get; set; }
		/*------------------------------------------------------------*/
		    
	}
}
