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
    [Route("api/source")]
    public class SourceController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Source";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public SourceController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Sources avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/source/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllSourcesPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Source.GetListAllSources()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Source.GetListAllSources().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Sources
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllSources()
        {
            try
            {
                var sources = _repWrapper.Source.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(sources);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Sources
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllSources()
        {
            try
            {
                var sources = _repWrapper.Source.GetListAllSources();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(sources);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Sources en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idSource"></param>
        [HttpGet("list/{idSource}")]
        public IActionResult GetListAllSourcesById(int idSource)
        {
            try
            {
                var sources = _repWrapper.Source.GetListAllSources(idSource);
                if (sources == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idSource);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(sources);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Source en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idSource"></param>
        [HttpGet("{idSource}")]
        public IActionResult GetSourceById(int idSource)
        {            
            try
            {
                var  source = _repWrapper.Source.FindByCondition(o => o.IdSource.Equals(idSource)).FirstOrDefault();
                if (source==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idSource);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idSource);
                    return Ok(source);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Source
        /// </summary>
        /// <returns></returns>
        /// <param name="source"></param>
        [HttpPost]
        public IActionResult AjouterSource([FromBody]Source source)
        {
            try
            {
                if (source == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Source.Create(source);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, source.IdSource.ToString());
                return Ok(source);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Source
        /// </summary>
        /// <returns></returns>
        /// <param name="idsource"></param>
        /// <param name="source"></param>
        [HttpPut("{idSource}")]
        public IActionResult MAJSource(int idsource, [FromBody]Source source)
        {
            try
            {
                if (source == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbsource = _repWrapper.Source.FindByCondition(o => o.IdSource.Equals(idsource)).FirstOrDefault();

                if (dbsource ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idsource);
                }                

                 _mapper.Map(source,dbsource);
                _repWrapper.Source.Update(dbsource);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idsource);
                return Ok(source);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Source
        /// </summary>
        /// <returns></returns>
        /// <param name="idsource"></param>
        [HttpDelete("{idSource}")]
        public IActionResult SupprimerSource(int idsource)
        {
            try
            {                
                Source sourceASupprimer = _repWrapper.Source.FindByCondition(o => o.IdSource == idsource).FirstOrDefault();

                if (sourceASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idsource);
                }
                
                _repWrapper.Source.Delete(sourceASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idsource);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Source
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetSourceCompte()
        {
            try
            {
                var compte = _repWrapper.Source.Count();
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
	
