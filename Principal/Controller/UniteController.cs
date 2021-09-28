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
    [Route("api/unite")]
    public class UniteController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Unite";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public UniteController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Unites avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/unite/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllUnitesPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Unite.GetListAllUnites()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Unite.GetListAllUnites().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Unites
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllUnites()
        {
            try
            {
                var unites = _repWrapper.Unite.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(unites);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Unites
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllUnites()
        {
            try
            {
                var unites = _repWrapper.Unite.GetListAllUnites();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(unites);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Unites en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idUnite"></param>
        [HttpGet("list/{idUnite}")]
        public IActionResult GetListAllUnitesById(int idUnite)
        {
            try
            {
                var unites = _repWrapper.Unite.GetListAllUnites(idUnite);
                if (unites == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idUnite);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(unites);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Unite en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idUnite"></param>
        [HttpGet("{idUnite}")]
        public IActionResult GetUniteById(int idUnite)
        {            
            try
            {
                var  unite = _repWrapper.Unite.FindByCondition(o => o.IdUnite.Equals(idUnite)).FirstOrDefault();
                if (unite==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idUnite);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idUnite);
                    return Ok(unite);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Unite
        /// </summary>
        /// <returns></returns>
        /// <param name="unite"></param>
        [HttpPost]
        public IActionResult AjouterUnite([FromBody]Unite unite)
        {
            try
            {
                if (unite == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Unite.Create(unite);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, unite.IdUnite.ToString());
                return Ok(unite);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Unite
        /// </summary>
        /// <returns></returns>
        /// <param name="idunite"></param>
        /// <param name="unite"></param>
        [HttpPut("{idUnite}")]
        public IActionResult MAJUnite(int idunite, [FromBody]Unite unite)
        {
            try
            {
                if (unite == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbunite = _repWrapper.Unite.FindByCondition(o => o.IdUnite.Equals(idunite)).FirstOrDefault();

                if (dbunite ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idunite);
                }                

                 _mapper.Map(unite,dbunite);
                _repWrapper.Unite.Update(dbunite);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idunite);
                return Ok(unite);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Unite
        /// </summary>
        /// <returns></returns>
        /// <param name="idunite"></param>
        [HttpDelete("{idUnite}")]
        public IActionResult SupprimerUnite(int idunite)
        {
            try
            {                
                Unite uniteASupprimer = _repWrapper.Unite.FindByCondition(o => o.IdUnite == idunite).FirstOrDefault();

                if (uniteASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idunite);
                }
                
                _repWrapper.Unite.Delete(uniteASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idunite);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Unite
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetUniteCompte()
        {
            try
            {
                var compte = _repWrapper.Unite.Count();
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
	
