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
    [Route("api/origine")]
    public class OrigineController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Origine";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public OrigineController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Origines avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/origine/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllOriginesPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Origine.GetListAllOrigines()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Origine.GetListAllOrigines().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Origines
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllOrigines()
        {
            try
            {
                var origines = _repWrapper.Origine.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(origines);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Origines
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllOrigines()
        {
            try
            {
                var origines = _repWrapper.Origine.GetListAllOrigines();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(origines);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Origines en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idOrigine"></param>
        [HttpGet("list/{idOrigine}")]
        public IActionResult GetListAllOriginesById(int idOrigine)
        {
            try
            {
                var origines = _repWrapper.Origine.GetListAllOrigines(idOrigine);
                if (origines == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idOrigine);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(origines);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Origine en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idOrigine"></param>
        [HttpGet("{idOrigine}")]
        public IActionResult GetOrigineById(int idOrigine)
        {            
            try
            {
                var  origine = _repWrapper.Origine.FindByCondition(o => o.IdOrigine.Equals(idOrigine)).FirstOrDefault();
                if (origine==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idOrigine);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idOrigine);
                    return Ok(origine);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Origine
        /// </summary>
        /// <returns></returns>
        /// <param name="origine"></param>
        [HttpPost]
        public IActionResult AjouterOrigine([FromBody]Origine origine)
        {
            try
            {
                if (origine == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Origine.Create(origine);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, origine.IdOrigine.ToString());
                return Ok(origine);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Origine
        /// </summary>
        /// <returns></returns>
        /// <param name="idorigine"></param>
        /// <param name="origine"></param>
        [HttpPut("{idOrigine}")]
        public IActionResult MAJOrigine(int idorigine, [FromBody]Origine origine)
        {
            try
            {
                if (origine == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dborigine = _repWrapper.Origine.FindByCondition(o => o.IdOrigine.Equals(idorigine)).FirstOrDefault();

                if (dborigine ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idorigine);
                }                

                 _mapper.Map(origine,dborigine);
                _repWrapper.Origine.Update(dborigine);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idorigine);
                return Ok(origine);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Origine
        /// </summary>
        /// <returns></returns>
        /// <param name="idorigine"></param>
        [HttpDelete("{idOrigine}")]
        public IActionResult SupprimerOrigine(int idorigine)
        {
            try
            {                
                Origine origineASupprimer = _repWrapper.Origine.FindByCondition(o => o.IdOrigine == idorigine).FirstOrDefault();

                if (origineASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idorigine);
                }
                
                _repWrapper.Origine.Delete(origineASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idorigine);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Origine
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetOrigineCompte()
        {
            try
            {
                var compte = _repWrapper.Origine.Count();
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
	
