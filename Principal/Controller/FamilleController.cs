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
    [Route("api/famille")]
    public class FamilleController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Famille";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public FamilleController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Familles avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/famille/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllFamillesPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Famille.GetListAllFamilles()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Famille.GetListAllFamilles().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Familles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllFamilles()
        {
            try
            {
                var familles = _repWrapper.Famille.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(familles);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Familles
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllFamilles()
        {
            try
            {
                var familles = _repWrapper.Famille.GetListAllFamilles();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(familles);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Familles en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idFamille"></param>
        [HttpGet("list/{idFamille}")]
        public IActionResult GetListAllFamillesById(int idFamille)
        {
            try
            {
                var familles = _repWrapper.Famille.GetListAllFamilles(idFamille);
                if (familles == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idFamille);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(familles);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Famille en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idFamille"></param>
        [HttpGet("{idFamille}")]
        public IActionResult GetFamilleById(int idFamille)
        {            
            try
            {
                var  famille = _repWrapper.Famille.FindByCondition(o => o.IdFamille.Equals(idFamille)).FirstOrDefault();
                if (famille==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idFamille);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idFamille);
                    return Ok(famille);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Famille
        /// </summary>
        /// <returns></returns>
        /// <param name="famille"></param>
        [HttpPost]
        public IActionResult AjouterFamille([FromBody]Famille famille)
        {
            try
            {
                if (famille == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Famille.Create(famille);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, famille.IdFamille.ToString());
                return Ok(famille);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Famille
        /// </summary>
        /// <returns></returns>
        /// <param name="idfamille"></param>
        /// <param name="famille"></param>
        [HttpPut("{idFamille}")]
        public IActionResult MAJFamille(int idfamille, [FromBody]Famille famille)
        {
            try
            {
                if (famille == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbfamille = _repWrapper.Famille.FindByCondition(o => o.IdFamille.Equals(idfamille)).FirstOrDefault();

                if (dbfamille ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idfamille);
                }                

                 _mapper.Map(famille,dbfamille);
                _repWrapper.Famille.Update(dbfamille);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idfamille);
                return Ok(famille);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Famille
        /// </summary>
        /// <returns></returns>
        /// <param name="idfamille"></param>
        [HttpDelete("{idFamille}")]
        public IActionResult SupprimerFamille(int idfamille)
        {
            try
            {                
                Famille familleASupprimer = _repWrapper.Famille.FindByCondition(o => o.IdFamille == idfamille).FirstOrDefault();

                if (familleASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idfamille);
                }
                
                _repWrapper.Famille.Delete(familleASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idfamille);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Famille
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetFamilleCompte()
        {
            try
            {
                var compte = _repWrapper.Famille.Count();
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
	
