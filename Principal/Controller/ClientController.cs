using System;
using System.Linq;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Principal.Divers;
using Principal.Divers.Pagination;

namespace Principal.Controllers
{    
    [Route("api/client")] 
    public class ClientController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Client";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public ClientController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Clients avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/client/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllClientsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Client.GetListAllClients()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Client.GetListAllClients().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllClients()
        {
            try
            {
                var clients = _repWrapper.Client.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Clients
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllClients()
        {
            try
            {
                var clients = _repWrapper.Client.GetListAllClients();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Clients en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idClient"></param>
        [HttpGet("list/{IdClient}")]
        public IActionResult GetListAllClientsById(int idClient)
        {
            try
            {
                var clients = _repWrapper.Client.GetListAllClients(idClient);
                if (clients == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idClient);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(clients);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Client en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idClient"></param>
        [HttpGet("{IdClient}")]
        public IActionResult GetClientById(int idClient)
        {            
            try
            {
                var  client = _repWrapper.Client.FindByCondition(o => o.IdClient.Equals(idClient)).FirstOrDefault();
                if (client==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idClient);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idClient);
                    return Ok(client);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Client
        /// </summary>
        /// <returns></returns>
        /// <param name="idclient"></param>
        [HttpPost]
        public IActionResult AjouterClient([FromBody]Client client)
        {
            try
            {
                if (client == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Client.Create(client);
                _repWrapper.Save();                
                return _msgeNotification.EntiteCreee(NomEntite, client.IdClient.ToString());
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Client
        /// </summary>
        /// <returns></returns>
        /// <param name="idclient"></param>
        /// <param name="client"></param>
        [HttpPut("{IdClient}")]
        public IActionResult MAJClient(int idclient, [FromBody]Client client)
        {
            try
            {
                if (client == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbclient = _repWrapper.Client.FindByCondition(o => o.IdClient.Equals(idclient)).FirstOrDefault();

                if (dbclient ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idclient);
                }                

                 _mapper.Map(client,dbclient);
                _repWrapper.Client.Update(dbclient);
                _repWrapper.Save();

                return _msgeNotification.EntiteMiseAJour(NomEntite, idclient);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Client
        /// </summary>
        /// <returns></returns>
        /// <param name="idclient"></param>
        [HttpDelete("{IdClient}")]
        public IActionResult SupprimerClient(int idclient)
        {
            try
            {                
                Client clientASupprimer = _repWrapper.Client.FindByCondition(o => o.IdClient == idclient).FirstOrDefault();

                if (clientASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idclient);
                }
                
                _repWrapper.Client.Delete(clientASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idclient);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Client
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetClientCompte()
        {
            try
            {
                var compte = _repWrapper.Client.Count();
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
	
