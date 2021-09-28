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
    [Route("api/facture")]
    public class FactureController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Facture";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public FactureController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Factures avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/facture/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllFacturesPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Facture.GetListAllFactures()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Facture.GetListAllFactures().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Factures
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllFactures()
        {
            try
            {
                var factures = _repWrapper.Facture.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(factures);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Factures
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllFactures()
        {
            try
            {
                var factures = _repWrapper.Facture.GetListAllFactures();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(factures);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Factures en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idFacture"></param>
        [HttpGet("list/{idFacture}")]
        public IActionResult GetListAllFacturesById(int idFacture)
        {
            try
            {
                var factures = _repWrapper.Facture.GetListAllFactures(idFacture);
                if (factures == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idFacture);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(factures);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Facture en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idFacture"></param>
        [HttpGet("{idFacture}")]
        public IActionResult GetFactureById(int idFacture)
        {            
            try
            {
                var  facture = _repWrapper.Facture.FindByCondition(o => o.IdFacture.Equals(idFacture)).FirstOrDefault();
                if (facture==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idFacture);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idFacture);
                    return Ok(facture);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Facture
        /// </summary>
        /// <returns></returns>
        /// <param name="facture"></param>
        [HttpPost]
        public IActionResult AjouterFacture([FromBody]Facture facture)
        {
            try
            {
                if (facture == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Facture.Create(facture);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, facture.IdFacture.ToString());
                return Ok(facture);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Facture
        /// </summary>
        /// <returns></returns>
        /// <param name="idfacture"></param>
        /// <param name="facture"></param>
        [HttpPut("{idFacture}")]
        public IActionResult MAJFacture(int idfacture, [FromBody]Facture facture)
        {
            try
            {
                if (facture == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbfacture = _repWrapper.Facture.FindByCondition(o => o.IdFacture.Equals(idfacture)).FirstOrDefault();

                if (dbfacture ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idfacture);
                }                

                 _mapper.Map(facture,dbfacture);
                _repWrapper.Facture.Update(dbfacture);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idfacture);
                return Ok(facture);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Facture
        /// </summary>
        /// <returns></returns>
        /// <param name="idfacture"></param>
        [HttpDelete("{idFacture}")]
        public IActionResult SupprimerFacture(int idfacture)
        {
            try
            {                
                Facture factureASupprimer = _repWrapper.Facture.FindByCondition(o => o.IdFacture == idfacture).FirstOrDefault();

                if (factureASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idfacture);
                }
                
                _repWrapper.Facture.Delete(factureASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idfacture);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Facture
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetFactureCompte()
        {
            try
            {
                var compte = _repWrapper.Facture.Count();
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
	
