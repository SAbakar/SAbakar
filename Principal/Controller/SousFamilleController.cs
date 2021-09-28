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
    [Route("api/sousfamille")]
    public class SousFamilleController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "SousFamille";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public SousFamilleController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les SousFamilles avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/sousfamille/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllSousFamillesPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.SousFamille.GetListAllSousFamilles()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.SousFamille.GetListAllSousFamilles().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les SousFamilles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllSousFamilles()
        {
            try
            {
                var sousfamilles = _repWrapper.SousFamille.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(sousfamilles);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les SousFamilles
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllSousFamilles()
        {
            try
            {
                var sousfamilles = _repWrapper.SousFamille.GetListAllSousFamilles();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(sousfamilles);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de SousFamilles en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idSousFamille"></param>
        [HttpGet("list/{idSousFamille}")]
        public IActionResult GetListAllSousFamillesById(int idSousFamille)
        {
            try
            {
                var sousfamilles = _repWrapper.SousFamille.GetListAllSousFamilles(idSousFamille);
                if (sousfamilles == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idSousFamille);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(sousfamilles);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) SousFamille en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idSousFamille"></param>
        [HttpGet("{idSousFamille}")]
        public IActionResult GetSousFamilleById(int idSousFamille)
        {            
            try
            {
                var  sousfamille = _repWrapper.SousFamille.FindByCondition(o => o.IdSousFamille.Equals(idSousFamille)).FirstOrDefault();
                if (sousfamille==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idSousFamille);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idSousFamille);
                    return Ok(sousfamille);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) SousFamille
        /// </summary>
        /// <returns></returns>
        /// <param name="sousfamille"></param>
        [HttpPost]
        public IActionResult AjouterSousFamille([FromBody]SousFamille sousfamille)
        {
            try
            {
                if (sousfamille == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.SousFamille.Create(sousfamille);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, sousfamille.IdSousFamille.ToString());
                return Ok(sousfamille);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) SousFamille
        /// </summary>
        /// <returns></returns>
        /// <param name="idsousfamille"></param>
        /// <param name="sousfamille"></param>
        [HttpPut("{idSousFamille}")]
        public IActionResult MAJSousFamille(int idsousfamille, [FromBody]SousFamille sousfamille)
        {
            try
            {
                if (sousfamille == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbsousfamille = _repWrapper.SousFamille.FindByCondition(o => o.IdSousFamille.Equals(idsousfamille)).FirstOrDefault();

                if (dbsousfamille ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idsousfamille);
                }                

                 _mapper.Map(sousfamille,dbsousfamille);
                _repWrapper.SousFamille.Update(dbsousfamille);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idsousfamille);
                return Ok(sousfamille);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) SousFamille
        /// </summary>
        /// <returns></returns>
        /// <param name="idsousfamille"></param>
        [HttpDelete("{idSousFamille}")]
        public IActionResult SupprimerSousFamille(int idsousfamille)
        {
            try
            {                
                SousFamille sousfamilleASupprimer = _repWrapper.SousFamille.FindByCondition(o => o.IdSousFamille == idsousfamille).FirstOrDefault();

                if (sousfamilleASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idsousfamille);
                }
                
                _repWrapper.SousFamille.Delete(sousfamilleASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idsousfamille);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de SousFamille
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetSousFamilleCompte()
        {
            try
            {
                var compte = _repWrapper.SousFamille.Count();
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
	
