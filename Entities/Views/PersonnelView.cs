using System;

namespace Entities.Views
{
    public class PersonnelView
    {
		public int IdPersonnel { get; set; }
		public string NomPersonnel { get; set; }
		public string PrenomPersonnel { get; set; }
		public string TelPersonnel { get; set; }
		public int IdFonction { get; set; }
		/*------------------  Proprietés Fonction ---------------------*/
		public string NomFonction{ get; set; }
		/*------------------------------------------------------------*/
		public int IdService { get; set; }
		/*------------------  Proprietés Service ---------------------*/
		public string NomService{ get; set; }
		/*------------------------------------------------------------*/
		    
	}
}
