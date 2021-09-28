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
    [Route("api/typecatalogue")]
    public class TypeCatalogueController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "TypeCatalogue";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public TypeCatalogueController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les TypeCatalogues avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/typecatalogue/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllTypeCataloguesPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.TypeCatalogue.GetListAllTypeCatalogues()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.TypeCatalogue.GetListAllTypeCatalogues().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les TypeCatalogues
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllTypeCatalogues()
        {
            try
            {
                var typecatalogues = _repWrapper.TypeCatalogue.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(typecatalogues);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les TypeCatalogues
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllTypeCatalogues()
        {
            try
            {
                var typecatalogues = _repWrapper.TypeCatalogue.GetListAllTypeCatalogues();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(typecatalogues);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de TypeCatalogues en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idTypeCatalogue"></param>
        [HttpGet("list/{idTypeCatalogue}")]
        public IActionResult GetListAllTypeCataloguesById(int idTypeCatalogue)
        {
            try
            {
                var typecatalogues = _repWrapper.TypeCatalogue.GetListAllTypeCatalogues(idTypeCatalogue);
                if (typecatalogues == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idTypeCatalogue);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(typecatalogues);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) TypeCatalogue en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idTypeCatalogue"></param>
        [HttpGet("{idTypeCatalogue}")]
        public IActionResult GetTypeCatalogueById(int idTypeCatalogue)
        {            
            try
            {
                var  typecatalogue = _repWrapper.TypeCatalogue.FindByCondition(o => o.IdTypeCatalogue.Equals(idTypeCatalogue)).FirstOrDefault();
                if (typecatalogue==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idTypeCatalogue);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idTypeCatalogue);
                    return Ok(typecatalogue);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) TypeCatalogue
        /// </summary>
        /// <returns></returns>
        /// <param name="typecatalogue"></param>
        [HttpPost]
        public IActionResult AjouterTypeCatalogue([FromBody]TypeCatalogue typecatalogue)
        {
            try
            {
                if (typecatalogue == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.TypeCatalogue.Create(typecatalogue);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, typecatalogue.IdTypeCatalogue.ToString());
                return Ok(typecatalogue);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) TypeCatalogue
        /// </summary>
        /// <returns></returns>
        /// <param name="idtypecatalogue"></param>
        /// <param name="typecatalogue"></param>
        [HttpPut("{idTypeCatalogue}")]
        public IActionResult MAJTypeCatalogue(int idtypecatalogue, [FromBody]TypeCatalogue typecatalogue)
        {
            try
            {
                if (typecatalogue == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbtypecatalogue = _repWrapper.TypeCatalogue.FindByCondition(o => o.IdTypeCatalogue.Equals(idtypecatalogue)).FirstOrDefault();

                if (dbtypecatalogue ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idtypecatalogue);
                }                

                 _mapper.Map(typecatalogue,dbtypecatalogue);
                _repWrapper.TypeCatalogue.Update(dbtypecatalogue);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idtypecatalogue);
                return Ok(typecatalogue);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) TypeCatalogue
        /// </summary>
        /// <returns></returns>
        /// <param name="idtypecatalogue"></param>
        [HttpDelete("{idTypeCatalogue}")]
        public IActionResult SupprimerTypeCatalogue(int idtypecatalogue)
        {
            try
            {                
                TypeCatalogue typecatalogueASupprimer = _repWrapper.TypeCatalogue.FindByCondition(o => o.IdTypeCatalogue == idtypecatalogue).FirstOrDefault();

                if (typecatalogueASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idtypecatalogue);
                }
                
                _repWrapper.TypeCatalogue.Delete(typecatalogueASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idtypecatalogue);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de TypeCatalogue
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetTypeCatalogueCompte()
        {
            try
            {
                var compte = _repWrapper.TypeCatalogue.Count();
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
	
