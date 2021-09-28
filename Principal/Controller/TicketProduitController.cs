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
    [Route("api/ticketproduit")]
    public class TicketProduitController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "TicketProduit";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public TicketProduitController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les TicketProduits avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/ticketproduit/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllTicketProduitsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.TicketProduit.GetListAllTicketProduits()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.TicketProduit.GetListAllTicketProduits().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les TicketProduits
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllTicketProduits()
        {
            try
            {
                var ticketproduits = _repWrapper.TicketProduit.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(ticketproduits);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les TicketProduits
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllTicketProduits()
        {
            try
            {
                var ticketproduits = _repWrapper.TicketProduit.GetListAllTicketProduits();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(ticketproduits);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de TicketProduits en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idTicketProduit"></param>
        [HttpGet("list/{idTicketProduit}")]
        public IActionResult GetListAllTicketProduitsById(int idTicketProduit)
        {
            try
            {
                var ticketproduits = _repWrapper.TicketProduit.GetListAllTicketProduits(idTicketProduit);
                if (ticketproduits == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idTicketProduit);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(ticketproduits);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) TicketProduit en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idTicketProduit"></param>
        [HttpGet("{idTicketProduit}")]
        public IActionResult GetTicketProduitById(int idTicketProduit)
        {            
            try
            {
                var  ticketproduit = _repWrapper.TicketProduit.FindByCondition(o => o.IdTicketProduit.Equals(idTicketProduit)).FirstOrDefault();
                if (ticketproduit==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idTicketProduit);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idTicketProduit);
                    return Ok(ticketproduit);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) TicketProduit
        /// </summary>
        /// <returns></returns>
        /// <param name="ticketproduit"></param>
        [HttpPost]
        public IActionResult AjouterTicketProduit([FromBody]TicketProduit ticketproduit)
        {
            try
            {
                if (ticketproduit == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.TicketProduit.Create(ticketproduit);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, ticketproduit.IdTicketProduit.ToString());
                return Ok(ticketproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) TicketProduit
        /// </summary>
        /// <returns></returns>
        /// <param name="idticketproduit"></param>
        /// <param name="ticketproduit"></param>
        [HttpPut("{idTicketProduit}")]
        public IActionResult MAJTicketProduit(int idticketproduit, [FromBody]TicketProduit ticketproduit)
        {
            try
            {
                if (ticketproduit == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbticketproduit = _repWrapper.TicketProduit.FindByCondition(o => o.IdTicketProduit.Equals(idticketproduit)).FirstOrDefault();

                if (dbticketproduit ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idticketproduit);
                }                

                 _mapper.Map(ticketproduit,dbticketproduit);
                _repWrapper.TicketProduit.Update(dbticketproduit);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idticketproduit);
                return Ok(ticketproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) TicketProduit
        /// </summary>
        /// <returns></returns>
        /// <param name="idticketproduit"></param>
        [HttpDelete("{idTicketProduit}")]
        public IActionResult SupprimerTicketProduit(int idticketproduit)
        {
            try
            {                
                TicketProduit ticketproduitASupprimer = _repWrapper.TicketProduit.FindByCondition(o => o.IdTicketProduit == idticketproduit).FirstOrDefault();

                if (ticketproduitASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idticketproduit);
                }
                
                _repWrapper.TicketProduit.Delete(ticketproduitASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idticketproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de TicketProduit
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetTicketProduitCompte()
        {
            try
            {
                var compte = _repWrapper.TicketProduit.Count();
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
	
