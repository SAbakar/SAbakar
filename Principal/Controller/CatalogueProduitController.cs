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
    [Route("api/catalogueproduit")]
    public class CatalogueProduitController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "CatalogueProduit";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public CatalogueProduitController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les CatalogueProduits avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/catalogueproduit/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllCatalogueProduitsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.CatalogueProduit.GetListAllCatalogueProduits()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.CatalogueProduit.GetListAllCatalogueProduits().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les CatalogueProduits
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllCatalogueProduits()
        {
            try
            {
                var catalogueproduits = _repWrapper.CatalogueProduit.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(catalogueproduits);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les CatalogueProduits
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllCatalogueProduits()
        {
            try
            {
                var catalogueproduits = _repWrapper.CatalogueProduit.GetListAllCatalogueProduits();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(catalogueproduits);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de CatalogueProduits en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idCatalogueProduit"></param>
        [HttpGet("list/{idCatalogueProduit}")]
        public IActionResult GetListAllCatalogueProduitsById(int idCatalogueProduit)
        {
            try
            {
                var catalogueproduits = _repWrapper.CatalogueProduit.GetListAllCatalogueProduits(idCatalogueProduit);
                if (catalogueproduits == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idCatalogueProduit);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(catalogueproduits);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) CatalogueProduit en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idCatalogueProduit"></param>
        [HttpGet("{idCatalogueProduit}")]
        public IActionResult GetCatalogueProduitById(int idCatalogueProduit)
        {            
            try
            {
                var  catalogueproduit = _repWrapper.CatalogueProduit.FindByCondition(o => o.IdCatalogueProduit.Equals(idCatalogueProduit)).FirstOrDefault();
                if (catalogueproduit==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idCatalogueProduit);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idCatalogueProduit);
                    return Ok(catalogueproduit);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) CatalogueProduit
        /// </summary>
        /// <returns></returns>
        /// <param name="catalogueproduit"></param>
        [HttpPost]
        public IActionResult AjouterCatalogueProduit([FromBody]CatalogueProduit catalogueproduit)
        {
            try
            {
                if (catalogueproduit == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.CatalogueProduit.Create(catalogueproduit);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, catalogueproduit.IdCatalogueProduit.ToString());
                return Ok(catalogueproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) CatalogueProduit
        /// </summary>
        /// <returns></returns>
        /// <param name="idcatalogueproduit"></param>
        /// <param name="catalogueproduit"></param>
        [HttpPut("{idCatalogueProduit}")]
        public IActionResult MAJCatalogueProduit(int idcatalogueproduit, [FromBody]CatalogueProduit catalogueproduit)
        {
            try
            {
                if (catalogueproduit == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbcatalogueproduit = _repWrapper.CatalogueProduit.FindByCondition(o => o.IdCatalogueProduit.Equals(idcatalogueproduit)).FirstOrDefault();

                if (dbcatalogueproduit ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idcatalogueproduit);
                }                

                 _mapper.Map(catalogueproduit,dbcatalogueproduit);
                _repWrapper.CatalogueProduit.Update(dbcatalogueproduit);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idcatalogueproduit);
                return Ok(catalogueproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) CatalogueProduit
        /// </summary>
        /// <returns></returns>
        /// <param name="idcatalogueproduit"></param>
        [HttpDelete("{idCatalogueProduit}")]
        public IActionResult SupprimerCatalogueProduit(int idcatalogueproduit)
        {
            try
            {                
                CatalogueProduit catalogueproduitASupprimer = _repWrapper.CatalogueProduit.FindByCondition(o => o.IdCatalogueProduit == idcatalogueproduit).FirstOrDefault();

                if (catalogueproduitASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idcatalogueproduit);
                }
                
                _repWrapper.CatalogueProduit.Delete(catalogueproduitASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idcatalogueproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de CatalogueProduit
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetCatalogueProduitCompte()
        {
            try
            {
                var compte = _repWrapper.CatalogueProduit.Count();
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
	
