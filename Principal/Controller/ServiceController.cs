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
    [Route("api/service")]
    public class ServiceController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Service";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public ServiceController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Services avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/service/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllServicesPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Service.GetListAllServices()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Service.GetListAllServices().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Services
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllServices()
        {
            try
            {
                var services = _repWrapper.Service.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(services);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Services
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllServices()
        {
            try
            {
                var services = _repWrapper.Service.GetListAllServices();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(services);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Services en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idService"></param>
        [HttpGet("list/{idService}")]
        public IActionResult GetListAllServicesById(int idService)
        {
            try
            {
                var services = _repWrapper.Service.GetListAllServices(idService);
                if (services == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idService);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(services);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Service en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idService"></param>
        [HttpGet("{idService}")]
        public IActionResult GetServiceById(int idService)
        {            
            try
            {
                var  service = _repWrapper.Service.FindByCondition(o => o.IdService.Equals(idService)).FirstOrDefault();
                if (service==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idService);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idService);
                    return Ok(service);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Service
        /// </summary>
        /// <returns></returns>
        /// <param name="service"></param>
        [HttpPost]
        public IActionResult AjouterService([FromBody]Service service)
        {
            try
            {
                if (service == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Service.Create(service);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, service.IdService.ToString());
                return Ok(service);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Service
        /// </summary>
        /// <returns></returns>
        /// <param name="idservice"></param>
        /// <param name="service"></param>
        [HttpPut("{idService}")]
        public IActionResult MAJService(int idservice, [FromBody]Service service)
        {
            try
            {
                if (service == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbservice = _repWrapper.Service.FindByCondition(o => o.IdService.Equals(idservice)).FirstOrDefault();

                if (dbservice ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idservice);
                }                

                 _mapper.Map(service,dbservice);
                _repWrapper.Service.Update(dbservice);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idservice);
                return Ok(service);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Service
        /// </summary>
        /// <returns></returns>
        /// <param name="idservice"></param>
        [HttpDelete("{idService}")]
        public IActionResult SupprimerService(int idservice)
        {
            try
            {                
                Service serviceASupprimer = _repWrapper.Service.FindByCondition(o => o.IdService == idservice).FirstOrDefault();

                if (serviceASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idservice);
                }
                
                _repWrapper.Service.Delete(serviceASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idservice);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Service
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetServiceCompte()
        {
            try
            {
                var compte = _repWrapper.Service.Count();
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
	
