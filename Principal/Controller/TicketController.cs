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
    [Route("api/ticket")]
    public class TicketController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Ticket";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public TicketController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Tickets avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/ticket/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllTicketsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Ticket.GetListAllTickets()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Ticket.GetListAllTickets().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Tickets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllTickets()
        {
            try
            {
                var tickets = _repWrapper.Ticket.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Tickets
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllTickets()
        {
            try
            {
                var tickets = _repWrapper.Ticket.GetListAllTickets();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Tickets en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idTicket"></param>
        [HttpGet("list/{idTicket}")]
        public IActionResult GetListAllTicketsById(int idTicket)
        {
            try
            {
                var tickets = _repWrapper.Ticket.GetListAllTickets(idTicket);
                if (tickets == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idTicket);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(tickets);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Ticket en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idTicket"></param>
        [HttpGet("{idTicket}")]
        public IActionResult GetTicketById(int idTicket)
        {            
            try
            {
                var  ticket = _repWrapper.Ticket.FindByCondition(o => o.IdTicket.Equals(idTicket)).FirstOrDefault();
                if (ticket==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idTicket);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idTicket);
                    return Ok(ticket);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Ticket
        /// </summary>
        /// <returns></returns>
        /// <param name="ticket"></param>
        [HttpPost]
        public IActionResult AjouterTicket([FromBody]Ticket ticket)
        {
            try
            {
                if (ticket == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Ticket.Create(ticket);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, ticket.IdTicket.ToString());
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Ticket
        /// </summary>
        /// <returns></returns>
        /// <param name="idticket"></param>
        /// <param name="ticket"></param>
        [HttpPut("{idTicket}")]
        public IActionResult MAJTicket(int idticket, [FromBody]Ticket ticket)
        {
            try
            {
                if (ticket == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbticket = _repWrapper.Ticket.FindByCondition(o => o.IdTicket.Equals(idticket)).FirstOrDefault();

                if (dbticket ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idticket);
                }                

                 _mapper.Map(ticket,dbticket);
                _repWrapper.Ticket.Update(dbticket);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idticket);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Ticket
        /// </summary>
        /// <returns></returns>
        /// <param name="idticket"></param>
        [HttpDelete("{idTicket}")]
        public IActionResult SupprimerTicket(int idticket)
        {
            try
            {                
                Ticket ticketASupprimer = _repWrapper.Ticket.FindByCondition(o => o.IdTicket == idticket).FirstOrDefault();

                if (ticketASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idticket);
                }
                
                _repWrapper.Ticket.Delete(ticketASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idticket);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Ticket
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetTicketCompte()
        {
            try
            {
                var compte = _repWrapper.Ticket.Count();
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
	
