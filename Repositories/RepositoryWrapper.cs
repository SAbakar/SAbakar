
using Contracts;
using Entities;

namespace Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDBContext _appDBContext;
        
                private IClientRepository _client;
                private IFamilleRepository _famille;
                private IFonctionRepository _fonction;
                private IMarqueRepository _marque;
                private IOrigineRepository _origine;
                private IServiceRepository _service;
                private ISourceRepository _source;
                private ISubstitutionProduitRepository _substitutionproduit;
                private ITypeBCIRepository _typebci;
                private ITypeCatalogueRepository _typecatalogue;
                private ITypeTicketRepository _typeticket;
                private IUniteRepository _unite;
                private IZoneStockageRepository _zonestockage;
                private ISousFamilleRepository _sousfamille;
                private IPersonnelRepository _personnel;
                private IFournisseurRepository _fournisseur;
                private ICatalogueRepository _catalogue;
                private IProduitRepository _produit;
                private IBCIRepository _bci;
                private ITicketRepository _ticket;
                private ICommandeFournisseurRepository _commandefournisseur;
                private ICatalogueProduitRepository _catalogueproduit;
                private IStockProduitRepository _stockproduit;
                private IProduitBCIRepository _produitbci;
                private IFactureRepository _facture;
                private ITicketProduitRepository _ticketproduit;
                private IProduitCommandeFournisseurRepository _produitcommandefournisseur;
        

        public RepositoryWrapper(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        
        
        public IClientRepository Client
        {
            get
            {
                if (_client == null)
                    _client = new ClientRepository(_appDBContext);
                return _client;
            }
        }

         
        public IFamilleRepository Famille
        {
            get
            {
                if (_famille == null)
                    _famille = new FamilleRepository(_appDBContext);
                return _famille;
            }
        }

         
        public IFonctionRepository Fonction
        {
            get
            {
                if (_fonction == null)
                    _fonction = new FonctionRepository(_appDBContext);
                return _fonction;
            }
        }

         
        public IMarqueRepository Marque
        {
            get
            {
                if (_marque == null)
                    _marque = new MarqueRepository(_appDBContext);
                return _marque;
            }
        }

         
        public IOrigineRepository Origine
        {
            get
            {
                if (_origine == null)
                    _origine = new OrigineRepository(_appDBContext);
                return _origine;
            }
        }

         
        public IServiceRepository Service
        {
            get
            {
                if (_service == null)
                    _service = new ServiceRepository(_appDBContext);
                return _service;
            }
        }

         
        public ISourceRepository Source
        {
            get
            {
                if (_source == null)
                    _source = new SourceRepository(_appDBContext);
                return _source;
            }
        }

         
        public ISubstitutionProduitRepository SubstitutionProduit
        {
            get
            {
                if (_substitutionproduit == null)
                    _substitutionproduit = new SubstitutionProduitRepository(_appDBContext);
                return _substitutionproduit;
            }
        }

         
        public ITypeBCIRepository TypeBCI
        {
            get
            {
                if (_typebci == null)
                    _typebci = new TypeBCIRepository(_appDBContext);
                return _typebci;
            }
        }

         
        public ITypeCatalogueRepository TypeCatalogue
        {
            get
            {
                if (_typecatalogue == null)
                    _typecatalogue = new TypeCatalogueRepository(_appDBContext);
                return _typecatalogue;
            }
        }

         
        public ITypeTicketRepository TypeTicket
        {
            get
            {
                if (_typeticket == null)
                    _typeticket = new TypeTicketRepository(_appDBContext);
                return _typeticket;
            }
        }

         
        public IUniteRepository Unite
        {
            get
            {
                if (_unite == null)
                    _unite = new UniteRepository(_appDBContext);
                return _unite;
            }
        }

         
        public IZoneStockageRepository ZoneStockage
        {
            get
            {
                if (_zonestockage == null)
                    _zonestockage = new ZoneStockageRepository(_appDBContext);
                return _zonestockage;
            }
        }

         
        public ISousFamilleRepository SousFamille
        {
            get
            {
                if (_sousfamille == null)
                    _sousfamille = new SousFamilleRepository(_appDBContext);
                return _sousfamille;
            }
        }

         
        public IPersonnelRepository Personnel
        {
            get
            {
                if (_personnel == null)
                    _personnel = new PersonnelRepository(_appDBContext);
                return _personnel;
            }
        }

         
        public IFournisseurRepository Fournisseur
        {
            get
            {
                if (_fournisseur == null)
                    _fournisseur = new FournisseurRepository(_appDBContext);
                return _fournisseur;
            }
        }

         
        public ICatalogueRepository Catalogue
        {
            get
            {
                if (_catalogue == null)
                    _catalogue = new CatalogueRepository(_appDBContext);
                return _catalogue;
            }
        }

         
        public IProduitRepository Produit
        {
            get
            {
                if (_produit == null)
                    _produit = new ProduitRepository(_appDBContext);
                return _produit;
            }
        }

         
        public IBCIRepository BCI
        {
            get
            {
                if (_bci == null)
                    _bci = new BCIRepository(_appDBContext);
                return _bci;
            }
        }

         
        public ITicketRepository Ticket
        {
            get
            {
                if (_ticket == null)
                    _ticket = new TicketRepository(_appDBContext);
                return _ticket;
            }
        }

         
        public ICommandeFournisseurRepository CommandeFournisseur
        {
            get
            {
                if (_commandefournisseur == null)
                    _commandefournisseur = new CommandeFournisseurRepository(_appDBContext);
                return _commandefournisseur;
            }
        }

         
        public ICatalogueProduitRepository CatalogueProduit
        {
            get
            {
                if (_catalogueproduit == null)
                    _catalogueproduit = new CatalogueProduitRepository(_appDBContext);
                return _catalogueproduit;
            }
        }

         
        public IStockProduitRepository StockProduit
        {
            get
            {
                if (_stockproduit == null)
                    _stockproduit = new StockProduitRepository(_appDBContext);
                return _stockproduit;
            }
        }

         
        public IProduitBCIRepository ProduitBCI
        {
            get
            {
                if (_produitbci == null)
                    _produitbci = new ProduitBCIRepository(_appDBContext);
                return _produitbci;
            }
        }

         
        public IFactureRepository Facture
        {
            get
            {
                if (_facture == null)
                    _facture = new FactureRepository(_appDBContext);
                return _facture;
            }
        }

         
        public ITicketProduitRepository TicketProduit
        {
            get
            {
                if (_ticketproduit == null)
                    _ticketproduit = new TicketProduitRepository(_appDBContext);
                return _ticketproduit;
            }
        }

         
        public IProduitCommandeFournisseurRepository ProduitCommandeFournisseur
        {
            get
            {
                if (_produitcommandefournisseur == null)
                    _produitcommandefournisseur = new ProduitCommandeFournisseurRepository(_appDBContext);
                return _produitcommandefournisseur;
            }
        }

         
        public void Save()
        {
            _appDBContext.SaveChanges();
        }

        public async void SaveAsync()
        {
           await _appDBContext.SaveChangesAsync();
        }

    }
}
	
