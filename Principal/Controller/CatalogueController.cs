using System;
using System.Linq;
using Principal.Divers;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Principal.Divers.Pagination;

namespace Principal.Controllers
{    
    [Route("api/catalogue")]
    public class CatalogueController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Catalogue";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public CatalogueController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Catalogues avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/catalogue/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllCataloguesPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Catalogue.GetListAllCatalogues()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Catalogue.GetListAllCatalogues().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Catalogues
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllCatalogues()
        {
            try
            {
                var catalogues = _repWrapper.Catalogue.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(catalogues);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Catalogues
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllCatalogues()
        {
            try
            {
                var catalogues = _repWrapper.Catalogue.GetListAllCatalogues();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(catalogues);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Catalogues en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idCatalogue"></param>
        [HttpGet("list/{idCatalogue}")]
        public IActionResult GetListAllCataloguesById(int idCatalogue)
        {
            try
            {
                var catalogues = _repWrapper.Catalogue.GetListAllCatalogues(idCatalogue);
                if (catalogues == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idCatalogue);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(catalogues);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Catalogue en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idCatalogue"></param>
        [HttpGet("{idCatalogue}")]
        public IActionResult GetCatalogueById(int idCatalogue)
        {            
            try
            {
                var  catalogue = _repWrapper.Catalogue.FindByCondition(o => o.IdCatalogue.Equals(idCatalogue)).FirstOrDefault();
                if (catalogue==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idCatalogue);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idCatalogue);
                    return Ok(catalogue);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Catalogue
        /// </summary>
        /// <returns></returns>
        /// <param name="catalogue"></param>
        [HttpPost]
        public IActionResult AjouterCatalogue([FromBody]Catalogue catalogue)
        {
            try
            {
                if (catalogue == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Catalogue.Create(catalogue);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, catalogue.IdCatalogue.ToString());
                return Ok(catalogue);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Catalogue
        /// </summary>
        /// <returns></returns>
        /// <param name="idcatalogue"></param>
        /// <param name="catalogue"></param>
        [HttpPut("{idCatalogue}")]
        public IActionResult MAJCatalogue(int idcatalogue, [FromBody]Catalogue catalogue)
        {
            try
            {
                if (catalogue == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbcatalogue = _repWrapper.Catalogue.FindByCondition(o => o.IdCatalogue.Equals(idcatalogue)).FirstOrDefault();

                if (dbcatalogue ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idcatalogue);
                }                

                 _mapper.Map(catalogue,dbcatalogue);
                _repWrapper.Catalogue.Update(dbcatalogue);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idcatalogue);
                return Ok(catalogue);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Catalogue
        /// </summary>
        /// <returns></returns>
        /// <param name="idcatalogue"></param>
        [HttpDelete("{idCatalogue}")]
        public IActionResult SupprimerCatalogue(int idcatalogue)
        {
            try
            {                
                Catalogue catalogueASupprimer = _repWrapper.Catalogue.FindByCondition(o => o.IdCatalogue == idcatalogue).FirstOrDefault();

                if (catalogueASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idcatalogue);
                }
                
                _repWrapper.Catalogue.Delete(catalogueASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idcatalogue);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Catalogue
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetCatalogueCompte()
        {
            try
            {
                var compte = _repWrapper.Catalogue.Count();
                _msgeNotification.EntiteCompte($"{NomEntite}s");
                return Ok(compte);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }        

    }
}
	
