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
    [Route("api/zonestockage")]
    public class ZoneStockageController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "ZoneStockage";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public ZoneStockageController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les ZoneStockages avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/zonestockage/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllZoneStockagesPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.ZoneStockage.GetListAllZoneStockages()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.ZoneStockage.GetListAllZoneStockages().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les ZoneStockages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllZoneStockages()
        {
            try
            {
                var zonestockages = _repWrapper.ZoneStockage.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(zonestockages);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les ZoneStockages
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllZoneStockages()
        {
            try
            {
                var zonestockages = _repWrapper.ZoneStockage.GetListAllZoneStockages();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(zonestockages);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de ZoneStockages en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idZoneStockage"></param>
        [HttpGet("list/{idZoneStockage}")]
        public IActionResult GetListAllZoneStockagesById(int idZoneStockage)
        {
            try
            {
                var zonestockages = _repWrapper.ZoneStockage.GetListAllZoneStockages(idZoneStockage);
                if (zonestockages == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idZoneStockage);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(zonestockages);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) ZoneStockage en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idZoneStockage"></param>
        [HttpGet("{idZoneStockage}")]
        public IActionResult GetZoneStockageById(int idZoneStockage)
        {            
            try
            {
                var  zonestockage = _repWrapper.ZoneStockage.FindByCondition(o => o.IdZoneStockage.Equals(idZoneStockage)).FirstOrDefault();
                if (zonestockage==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idZoneStockage);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idZoneStockage);
                    return Ok(zonestockage);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) ZoneStockage
        /// </summary>
        /// <returns></returns>
        /// <param name="zonestockage"></param>
        [HttpPost]
        public IActionResult AjouterZoneStockage([FromBody]ZoneStockage zonestockage)
        {
            try
            {
                if (zonestockage == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.ZoneStockage.Create(zonestockage);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, zonestockage.IdZoneStockage.ToString());
                return Ok(zonestockage);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) ZoneStockage
        /// </summary>
        /// <returns></returns>
        /// <param name="idzonestockage"></param>
        /// <param name="zonestockage"></param>
        [HttpPut("{idZoneStockage}")]
        public IActionResult MAJZoneStockage(int idzonestockage, [FromBody]ZoneStockage zonestockage)
        {
            try
            {
                if (zonestockage == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbzonestockage = _repWrapper.ZoneStockage.FindByCondition(o => o.IdZoneStockage.Equals(idzonestockage)).FirstOrDefault();

                if (dbzonestockage ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idzonestockage);
                }                

                 _mapper.Map(zonestockage,dbzonestockage);
                _repWrapper.ZoneStockage.Update(dbzonestockage);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idzonestockage);
                return Ok(zonestockage);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) ZoneStockage
        /// </summary>
        /// <returns></returns>
        /// <param name="idzonestockage"></param>
        [HttpDelete("{idZoneStockage}")]
        public IActionResult SupprimerZoneStockage(int idzonestockage)
        {
            try
            {                
                ZoneStockage zonestockageASupprimer = _repWrapper.ZoneStockage.FindByCondition(o => o.IdZoneStockage == idzonestockage).FirstOrDefault();

                if (zonestockageASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idzonestockage);
                }
                
                _repWrapper.ZoneStockage.Delete(zonestockageASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idzonestockage);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de ZoneStockage
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetZoneStockageCompte()
        {
            try
            {
                var compte = _repWrapper.ZoneStockage.Count();
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
	
