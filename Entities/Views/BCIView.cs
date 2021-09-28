using System;

namespace Entities.Views
{
    public class BCIView
    {
		public int IdBCI { get; set; }
		public string NumeroBCI { get; set; }
		public DateTime DateBCI { get; set; }
		public DateTime DateValidationBCI { get; set; }
		public bool IsValider { get; set; }
		public bool IsAnnuler { get; set; }
		public string ObsBCI { get; set; }
		public int IdPersonnel { get; set; }
		/*------------------  Proprietés Personnel ---------------------*/
		public string NomPersonnel{ get; set; }
		public string PrenomPersonnel{ get; set; }
		public string TelPersonnel{ get; set; }
		/*------------------------------------------------------------*/
		public int IdTypeBCI { get; set; }
		/*------------------  Proprietés TypeBCI ---------------------*/
		public string NomTypeBCI{ get; set; }
		/*------------------------------------------------------------*/
		    
	}
}
