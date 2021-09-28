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
    [Route("api/produitbci")]
    public class ProduitBCIController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "ProduitBCI";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public ProduitBCIController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les ProduitBCIs avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/produitbci/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllProduitBCIsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.ProduitBCI.GetListAllProduitBCIs()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.ProduitBCI.GetListAllProduitBCIs().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les ProduitBCIs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllProduitBCIs()
        {
            try
            {
                var produitbcis = _repWrapper.ProduitBCI.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(produitbcis);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les ProduitBCIs
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllProduitBCIs()
        {
            try
            {
                var produitbcis = _repWrapper.ProduitBCI.GetListAllProduitBCIs();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(produitbcis);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de ProduitBCIs en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idProduitBCI"></param>
        [HttpGet("list/{idProduitBCI}")]
        public IActionResult GetListAllProduitBCIsById(int idProduitBCI)
        {
            try
            {
                var produitbcis = _repWrapper.ProduitBCI.GetListAllProduitBCIs(idProduitBCI);
                if (produitbcis == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idProduitBCI);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(produitbcis);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) ProduitBCI en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idProduitBCI"></param>
        [HttpGet("{idProduitBCI}")]
        public IActionResult GetProduitBCIById(int idProduitBCI)
        {            
            try
            {
                var  produitbci = _repWrapper.ProduitBCI.FindByCondition(o => o.IdProduitBCI.Equals(idProduitBCI)).FirstOrDefault();
                if (produitbci==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idProduitBCI);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idProduitBCI);
                    return Ok(produitbci);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) ProduitBCI
        /// </summary>
        /// <returns></returns>
        /// <param name="produitbci"></param>
        [HttpPost]
        public IActionResult AjouterProduitBCI([FromBody]ProduitBCI produitbci)
        {
            try
            {
                if (produitbci == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.ProduitBCI.Create(produitbci);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, produitbci.IdProduitBCI.ToString());
                return Ok(produitbci);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) ProduitBCI
        /// </summary>
        /// <returns></returns>
        /// <param name="idproduitbci"></param>
        /// <param name="produitbci"></param>
        [HttpPut("{idProduitBCI}")]
        public IActionResult MAJProduitBCI(int idproduitbci, [FromBody]ProduitBCI produitbci)
        {
            try
            {
                if (produitbci == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbproduitbci = _repWrapper.ProduitBCI.FindByCondition(o => o.IdProduitBCI.Equals(idproduitbci)).FirstOrDefault();

                if (dbproduitbci ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idproduitbci);
                }                

                 _mapper.Map(produitbci,dbproduitbci);
                _repWrapper.ProduitBCI.Update(dbproduitbci);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idproduitbci);
                return Ok(produitbci);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) ProduitBCI
        /// </summary>
        /// <returns></returns>
        /// <param name="idproduitbci"></param>
        [HttpDelete("{idProduitBCI}")]
        public IActionResult SupprimerProduitBCI(int idproduitbci)
        {
            try
            {                
                ProduitBCI produitbciASupprimer = _repWrapper.ProduitBCI.FindByCondition(o => o.IdProduitBCI == idproduitbci).FirstOrDefault();

                if (produitbciASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idproduitbci);
                }
                
                _repWrapper.ProduitBCI.Delete(produitbciASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idproduitbci);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de ProduitBCI
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetProduitBCICompte()
        {
            try
            {
                var compte = _repWrapper.ProduitBCI.Count();
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
	
