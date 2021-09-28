using System;

namespace Entities.Views
{
    public class CatalogueView
    {
		public int IdCatalogue { get; set; }
		public string NomCatalogue { get; set; }
		public int IdClientService { get; set; }
		public int IdTypeCatalogue { get; set; }
		/*------------------  Proprietés TypeCatalogue ---------------------*/
		public string NomTypeCatalogue{ get; set; }
		/*------------------------------------------------------------*/
		    
	}
}
