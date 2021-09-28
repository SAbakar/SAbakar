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
    [Route("api/bci")]
    public class BCIController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "BCI";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public BCIController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les BCIs avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/bci/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllBCIsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.BCI.GetListAllBCIs()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.BCI.GetListAllBCIs().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les BCIs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllBCIs()
        {
            try
            {
                var bcis = _repWrapper.BCI.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(bcis);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les BCIs
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllBCIs()
        {
            try
            {
                var bcis = _repWrapper.BCI.GetListAllBCIs();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(bcis);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de BCIs en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idBCI"></param>
        [HttpGet("list/{idBCI}")]
        public IActionResult GetListAllBCIsById(int idBCI)
        {
            try
            {
                var bcis = _repWrapper.BCI.GetListAllBCIs(idBCI);
                if (bcis == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idBCI);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(bcis);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) BCI en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idBCI"></param>
        [HttpGet("{idBCI}")]
        public IActionResult GetBCIById(int idBCI)
        {            
            try
            {
                var  bci = _repWrapper.BCI.FindByCondition(o => o.IdBCI.Equals(idBCI)).FirstOrDefault();
                if (bci==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idBCI);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idBCI);
                    return Ok(bci);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) BCI
        /// </summary>
        /// <returns></returns>
        /// <param name="bci"></param>
        [HttpPost]
        public IActionResult AjouterBCI([FromBody]BCI bci)
        {
            try
            {
                if (bci == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.BCI.Create(bci);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, bci.IdBCI.ToString());
                return Ok(bci);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) BCI
        /// </summary>
        /// <returns></returns>
        /// <param name="idbci"></param>
        /// <param name="bci"></param>
        [HttpPut("{idBCI}")]
        public IActionResult MAJBCI(int idbci, [FromBody]BCI bci)
        {
            try
            {
                if (bci == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbbci = _repWrapper.BCI.FindByCondition(o => o.IdBCI.Equals(idbci)).FirstOrDefault();

                if (dbbci ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idbci);
                }                

                 _mapper.Map(bci,dbbci);
                _repWrapper.BCI.Update(dbbci);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idbci);
                return Ok(bci);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) BCI
        /// </summary>
        /// <returns></returns>
        /// <param name="idbci"></param>
        [HttpDelete("{idBCI}")]
        public IActionResult SupprimerBCI(int idbci)
        {
            try
            {                
                BCI bciASupprimer = _repWrapper.BCI.FindByCondition(o => o.IdBCI == idbci).FirstOrDefault();

                if (bciASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idbci);
                }
                
                _repWrapper.BCI.Delete(bciASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idbci);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de BCI
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetBCICompte()
        {
            try
            {
                var compte = _repWrapper.BCI.Count();
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
	
