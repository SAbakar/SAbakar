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
    [Route("api/typeticket")]
    public class TypeTicketController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "TypeTicket";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public TypeTicketController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les TypeTickets avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/typeticket/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllTypeTicketsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.TypeTicket.GetListAllTypeTickets()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.TypeTicket.GetListAllTypeTickets().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les TypeTickets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllTypeTickets()
        {
            try
            {
                var typetickets = _repWrapper.TypeTicket.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(typetickets);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les TypeTickets
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllTypeTickets()
        {
            try
            {
                var typetickets = _repWrapper.TypeTicket.GetListAllTypeTickets();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(typetickets);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de TypeTickets en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idTypeTicket"></param>
        [HttpGet("list/{idTypeTicket}")]
        public IActionResult GetListAllTypeTicketsById(int idTypeTicket)
        {
            try
            {
                var typetickets = _repWrapper.TypeTicket.GetListAllTypeTickets(idTypeTicket);
                if (typetickets == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idTypeTicket);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(typetickets);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) TypeTicket en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idTypeTicket"></param>
        [HttpGet("{idTypeTicket}")]
        public IActionResult GetTypeTicketById(int idTypeTicket)
        {            
            try
            {
                var  typeticket = _repWrapper.TypeTicket.FindByCondition(o => o.IdTypeTicket.Equals(idTypeTicket)).FirstOrDefault();
                if (typeticket==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idTypeTicket);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idTypeTicket);
                    return Ok(typeticket);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) TypeTicket
        /// </summary>
        /// <returns></returns>
        /// <param name="typeticket"></param>
        [HttpPost]
        public IActionResult AjouterTypeTicket([FromBody]TypeTicket typeticket)
        {
            try
            {
                if (typeticket == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.TypeTicket.Create(typeticket);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, typeticket.IdTypeTicket.ToString());
                return Ok(typeticket);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) TypeTicket
        /// </summary>
        /// <returns></returns>
        /// <param name="idtypeticket"></param>
        /// <param name="typeticket"></param>
        [HttpPut("{idTypeTicket}")]
        public IActionResult MAJTypeTicket(int idtypeticket, [FromBody]TypeTicket typeticket)
        {
            try
            {
                if (typeticket == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbtypeticket = _repWrapper.TypeTicket.FindByCondition(o => o.IdTypeTicket.Equals(idtypeticket)).FirstOrDefault();

                if (dbtypeticket ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idtypeticket);
                }                

                 _mapper.Map(typeticket,dbtypeticket);
                _repWrapper.TypeTicket.Update(dbtypeticket);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idtypeticket);
                return Ok(typeticket);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) TypeTicket
        /// </summary>
        /// <returns></returns>
        /// <param name="idtypeticket"></param>
        [HttpDelete("{idTypeTicket}")]
        public IActionResult SupprimerTypeTicket(int idtypeticket)
        {
            try
            {                
                TypeTicket typeticketASupprimer = _repWrapper.TypeTicket.FindByCondition(o => o.IdTypeTicket == idtypeticket).FirstOrDefault();

                if (typeticketASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idtypeticket);
                }
                
                _repWrapper.TypeTicket.Delete(typeticketASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idtypeticket);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de TypeTicket
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetTypeTicketCompte()
        {
            try
            {
                var compte = _repWrapper.TypeTicket.Count();
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
	
