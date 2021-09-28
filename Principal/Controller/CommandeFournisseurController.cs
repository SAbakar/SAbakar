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
    [Route("api/commandefournisseur")]
    public class CommandeFournisseurController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "CommandeFournisseur";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public CommandeFournisseurController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les CommandeFournisseurs avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/commandefournisseur/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllCommandeFournisseursPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.CommandeFournisseur.GetListAllCommandeFournisseurs()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.CommandeFournisseur.GetListAllCommandeFournisseurs().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les CommandeFournisseurs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllCommandeFournisseurs()
        {
            try
            {
                var commandefournisseurs = _repWrapper.CommandeFournisseur.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(commandefournisseurs);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les CommandeFournisseurs
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllCommandeFournisseurs()
        {
            try
            {
                var commandefournisseurs = _repWrapper.CommandeFournisseur.GetListAllCommandeFournisseurs();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(commandefournisseurs);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de CommandeFournisseurs en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idCommandeFournisseur"></param>
        [HttpGet("list/{idCommandeFournisseur}")]
        public IActionResult GetListAllCommandeFournisseursById(int idCommandeFournisseur)
        {
            try
            {
                var commandefournisseurs = _repWrapper.CommandeFournisseur.GetListAllCommandeFournisseurs(idCommandeFournisseur);
                if (commandefournisseurs == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idCommandeFournisseur);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(commandefournisseurs);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) CommandeFournisseur en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idCommandeFournisseur"></param>
        [HttpGet("{idCommandeFournisseur}")]
        public IActionResult GetCommandeFournisseurById(int idCommandeFournisseur)
        {            
            try
            {
                var  commandefournisseur = _repWrapper.CommandeFournisseur.FindByCondition(o => o.IdCommandeFournisseur.Equals(idCommandeFournisseur)).FirstOrDefault();
                if (commandefournisseur==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idCommandeFournisseur);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idCommandeFournisseur);
                    return Ok(commandefournisseur);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) CommandeFournisseur
        /// </summary>
        /// <returns></returns>
        /// <param name="commandefournisseur"></param>
        [HttpPost]
        public IActionResult AjouterCommandeFournisseur([FromBody]CommandeFournisseur commandefournisseur)
        {
            try
            {
                if (commandefournisseur == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.CommandeFournisseur.Create(commandefournisseur);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, commandefournisseur.IdCommandeFournisseur.ToString());
                return Ok(commandefournisseur);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) CommandeFournisseur
        /// </summary>
        /// <returns></returns>
        /// <param name="idcommandefournisseur"></param>
        /// <param name="commandefournisseur"></param>
        [HttpPut("{idCommandeFournisseur}")]
        public IActionResult MAJCommandeFournisseur(int idcommandefournisseur, [FromBody]CommandeFournisseur commandefournisseur)
        {
            try
            {
                if (commandefournisseur == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbcommandefournisseur = _repWrapper.CommandeFournisseur.FindByCondition(o => o.IdCommandeFournisseur.Equals(idcommandefournisseur)).FirstOrDefault();

                if (dbcommandefournisseur ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idcommandefournisseur);
                }                

                 _mapper.Map(commandefournisseur,dbcommandefournisseur);
                _repWrapper.CommandeFournisseur.Update(dbcommandefournisseur);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idcommandefournisseur);
                return Ok(commandefournisseur);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) CommandeFournisseur
        /// </summary>
        /// <returns></returns>
        /// <param name="idcommandefournisseur"></param>
        [HttpDelete("{idCommandeFournisseur}")]
        public IActionResult SupprimerCommandeFournisseur(int idcommandefournisseur)
        {
            try
            {                
                CommandeFournisseur commandefournisseurASupprimer = _repWrapper.CommandeFournisseur.FindByCondition(o => o.IdCommandeFournisseur == idcommandefournisseur).FirstOrDefault();

                if (commandefournisseurASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idcommandefournisseur);
                }
                
                _repWrapper.CommandeFournisseur.Delete(commandefournisseurASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idcommandefournisseur);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de CommandeFournisseur
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetCommandeFournisseurCompte()
        {
            try
            {
                var compte = _repWrapper.CommandeFournisseur.Count();
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
	
