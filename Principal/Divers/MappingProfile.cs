using AutoMapper;
using Entities.Models;

namespace Principal.Divers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
                        CreateMap <Client, Client>().ForMember(d => d.IdClient, a => a.Ignore());
                        CreateMap <Famille, Famille>().ForMember(d => d.IdFamille, a => a.Ignore());
                        CreateMap <Fonction, Fonction>().ForMember(d => d.IdFonction, a => a.Ignore());
                        CreateMap <Marque, Marque>().ForMember(d => d.IdMarque, a => a.Ignore());
                        CreateMap <Origine, Origine>().ForMember(d => d.IdOrigine, a => a.Ignore());
                        CreateMap <Service, Service>().ForMember(d => d.IdService, a => a.Ignore());
                        CreateMap <Source, Source>().ForMember(d => d.IdSource, a => a.Ignore());
                        CreateMap <SubstitutionProduit, SubstitutionProduit>().ForMember(d => d.IdSubstitutionProduit, a => a.Ignore());
                        CreateMap <TypeBCI, TypeBCI>().ForMember(d => d.IdTypeBCI, a => a.Ignore());
                        CreateMap <TypeCatalogue, TypeCatalogue>().ForMember(d => d.IdTypeCatalogue, a => a.Ignore());
                        CreateMap <TypeTicket, TypeTicket>().ForMember(d => d.IdTypeTicket, a => a.Ignore());
                        CreateMap <Unite, Unite>().ForMember(d => d.IdUnite, a => a.Ignore());
                        CreateMap <ZoneStockage, ZoneStockage>().ForMember(d => d.IdZoneStockage, a => a.Ignore());
                        CreateMap <SousFamille, SousFamille>().ForMember(d => d.IdSousFamille, a => a.Ignore());
                        CreateMap <Personnel, Personnel>().ForMember(d => d.IdPersonnel, a => a.Ignore());
                        CreateMap <Fournisseur, Fournisseur>().ForMember(d => d.IdFournisseur, a => a.Ignore());
                        CreateMap <Catalogue, Catalogue>().ForMember(d => d.IdCatalogue, a => a.Ignore());
                        CreateMap <Produit, Produit>().ForMember(d => d.IdProduit, a => a.Ignore());
                        CreateMap <BCI, BCI>().ForMember(d => d.IdBCI, a => a.Ignore());
                        CreateMap <Ticket, Ticket>().ForMember(d => d.IdTicket, a => a.Ignore());
                        CreateMap <CommandeFournisseur, CommandeFournisseur>().ForMember(d => d.IdCommandeFournisseur, a => a.Ignore());
                        CreateMap <CatalogueProduit, CatalogueProduit>().ForMember(d => d.IdCatalogueProduit, a => a.Ignore());
                        CreateMap <StockProduit, StockProduit>().ForMember(d => d.IdStockProduit, a => a.Ignore());
                        CreateMap <ProduitBCI, ProduitBCI>().ForMember(d => d.IdProduitBCI, a => a.Ignore());
                        CreateMap <Facture, Facture>().ForMember(d => d.IdFacture, a => a.Ignore());
                        CreateMap <TicketProduit, TicketProduit>().ForMember(d => d.IdTicketProduit, a => a.Ignore());
                        CreateMap <ProduitCommandeFournisseur, ProduitCommandeFournisseur>().ForMember(d => d.IdProduitCommandeFournisseur, a => a.Ignore());
              
        }
    }
}
