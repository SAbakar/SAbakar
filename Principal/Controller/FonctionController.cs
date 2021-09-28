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
    [Route("api/fonction")]
    public class FonctionController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Fonction";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public FonctionController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Fonctions avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/fonction/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllFonctionsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Fonction.GetListAllFonctions()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Fonction.GetListAllFonctions().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Fonctions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllFonctions()
        {
            try
            {
                var fonctions = _repWrapper.Fonction.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(fonctions);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Fonctions
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllFonctions()
        {
            try
            {
                var fonctions = _repWrapper.Fonction.GetListAllFonctions();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(fonctions);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Fonctions en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idFonction"></param>
        [HttpGet("list/{idFonction}")]
        public IActionResult GetListAllFonctionsById(int idFonction)
        {
            try
            {
                var fonctions = _repWrapper.Fonction.GetListAllFonctions(idFonction);
                if (fonctions == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idFonction);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(fonctions);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Fonction en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idFonction"></param>
        [HttpGet("{idFonction}")]
        public IActionResult GetFonctionById(int idFonction)
        {            
            try
            {
                var  fonction = _repWrapper.Fonction.FindByCondition(o => o.IdFonction.Equals(idFonction)).FirstOrDefault();
                if (fonction==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idFonction);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idFonction);
                    return Ok(fonction);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Fonction
        /// </summary>
        /// <returns></returns>
        /// <param name="fonction"></param>
        [HttpPost]
        public IActionResult AjouterFonction([FromBody]Fonction fonction)
        {
            try
            {
                if (fonction == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Fonction.Create(fonction);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, fonction.IdFonction.ToString());
                return Ok(fonction);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Fonction
        /// </summary>
        /// <returns></returns>
        /// <param name="idfonction"></param>
        /// <param name="fonction"></param>
        [HttpPut("{idFonction}")]
        public IActionResult MAJFonction(int idfonction, [FromBody]Fonction fonction)
        {
            try
            {
                if (fonction == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbfonction = _repWrapper.Fonction.FindByCondition(o => o.IdFonction.Equals(idfonction)).FirstOrDefault();

                if (dbfonction ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idfonction);
                }                

                 _mapper.Map(fonction,dbfonction);
                _repWrapper.Fonction.Update(dbfonction);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idfonction);
                return Ok(fonction);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Fonction
        /// </summary>
        /// <returns></returns>
        /// <param name="idfonction"></param>
        [HttpDelete("{idFonction}")]
        public IActionResult SupprimerFonction(int idfonction)
        {
            try
            {                
                Fonction fonctionASupprimer = _repWrapper.Fonction.FindByCondition(o => o.IdFonction == idfonction).FirstOrDefault();

                if (fonctionASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idfonction);
                }
                
                _repWrapper.Fonction.Delete(fonctionASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idfonction);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Fonction
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetFonctionCompte()
        {
            try
            {
                var compte = _repWrapper.Fonction.Count();
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
	
