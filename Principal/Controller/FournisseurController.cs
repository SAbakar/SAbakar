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
    [Route("api/fournisseur")]
    public class FournisseurController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Fournisseur";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public FournisseurController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Fournisseurs avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/fournisseur/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllFournisseursPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Fournisseur.GetListAllFournisseurs()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Fournisseur.GetListAllFournisseurs().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Fournisseurs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllFournisseurs()
        {
            try
            {
                var fournisseurs = _repWrapper.Fournisseur.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(fournisseurs);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Fournisseurs
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllFournisseurs()
        {
            try
            {
                var fournisseurs = _repWrapper.Fournisseur.GetListAllFournisseurs();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(fournisseurs);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Fournisseurs en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idFournisseur"></param>
        [HttpGet("list/{idFournisseur}")]
        public IActionResult GetListAllFournisseursById(int idFournisseur)
        {
            try
            {
                var fournisseurs = _repWrapper.Fournisseur.GetListAllFournisseurs(idFournisseur);
                if (fournisseurs == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idFournisseur);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(fournisseurs);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Fournisseur en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idFournisseur"></param>
        [HttpGet("{idFournisseur}")]
        public IActionResult GetFournisseurById(int idFournisseur)
        {            
            try
            {
                var  fournisseur = _repWrapper.Fournisseur.FindByCondition(o => o.IdFournisseur.Equals(idFournisseur)).FirstOrDefault();
                if (fournisseur==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idFournisseur);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idFournisseur);
                    return Ok(fournisseur);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Fournisseur
        /// </summary>
        /// <returns></returns>
        /// <param name="fournisseur"></param>
        [HttpPost]
        public IActionResult AjouterFournisseur([FromBody]Fournisseur fournisseur)
        {
            try
            {
                if (fournisseur == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Fournisseur.Create(fournisseur);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, fournisseur.IdFournisseur.ToString());
                return Ok(fournisseur);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Fournisseur
        /// </summary>
        /// <returns></returns>
        /// <param name="idfournisseur"></param>
        /// <param name="fournisseur"></param>
        [HttpPut("{idFournisseur}")]
        public IActionResult MAJFournisseur(int idfournisseur, [FromBody]Fournisseur fournisseur)
        {
            try
            {
                if (fournisseur == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbfournisseur = _repWrapper.Fournisseur.FindByCondition(o => o.IdFournisseur.Equals(idfournisseur)).FirstOrDefault();

                if (dbfournisseur ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idfournisseur);
                }                

                 _mapper.Map(fournisseur,dbfournisseur);
                _repWrapper.Fournisseur.Update(dbfournisseur);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idfournisseur);
                return Ok(fournisseur);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Fournisseur
        /// </summary>
        /// <returns></returns>
        /// <param name="idfournisseur"></param>
        [HttpDelete("{idFournisseur}")]
        public IActionResult SupprimerFournisseur(int idfournisseur)
        {
            try
            {                
                Fournisseur fournisseurASupprimer = _repWrapper.Fournisseur.FindByCondition(o => o.IdFournisseur == idfournisseur).FirstOrDefault();

                if (fournisseurASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idfournisseur);
                }
                
                _repWrapper.Fournisseur.Delete(fournisseurASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idfournisseur);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Fournisseur
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetFournisseurCompte()
        {
            try
            {
                var compte = _repWrapper.Fournisseur.Count();
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
	
