
namespace Contracts
{
    public interface IRepositoryWrapper
    {
                IClientRepository Client {get;}
                IFamilleRepository Famille {get;}
                IFonctionRepository Fonction {get;}
                IMarqueRepository Marque {get;}
                IOrigineRepository Origine {get;}
                IServiceRepository Service {get;}
                ISourceRepository Source {get;}
                ISubstitutionProduitRepository SubstitutionProduit {get;}
                ITypeBCIRepository TypeBCI {get;}
                ITypeCatalogueRepository TypeCatalogue {get;}
                ITypeTicketRepository TypeTicket {get;}
                IUniteRepository Unite {get;}
                IZoneStockageRepository ZoneStockage {get;}
                ISousFamilleRepository SousFamille {get;}
                IPersonnelRepository Personnel {get;}
                IFournisseurRepository Fournisseur {get;}
                ICatalogueRepository Catalogue {get;}
                IProduitRepository Produit {get;}
                IBCIRepository BCI {get;}
                ITicketRepository Ticket {get;}
                ICommandeFournisseurRepository CommandeFournisseur {get;}
                ICatalogueProduitRepository CatalogueProduit {get;}
                IStockProduitRepository StockProduit {get;}
                IProduitBCIRepository ProduitBCI {get;}
                IFactureRepository Facture {get;}
                ITicketProduitRepository TicketProduit {get;}
                IProduitCommandeFournisseurRepository ProduitCommandeFournisseur {get;}
                void Save();
    }
}
	
