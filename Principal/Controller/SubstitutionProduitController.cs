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
    [Route("api/substitutionproduit")]
    public class SubstitutionProduitController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "SubstitutionProduit";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public SubstitutionProduitController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les SubstitutionProduits avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/substitutionproduit/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllSubstitutionProduitsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.SubstitutionProduit.GetListAllSubstitutionProduits()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.SubstitutionProduit.GetListAllSubstitutionProduits().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les SubstitutionProduits
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllSubstitutionProduits()
        {
            try
            {
                var substitutionproduits = _repWrapper.SubstitutionProduit.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(substitutionproduits);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les SubstitutionProduits
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllSubstitutionProduits()
        {
            try
            {
                var substitutionproduits = _repWrapper.SubstitutionProduit.GetListAllSubstitutionProduits();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(substitutionproduits);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de SubstitutionProduits en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idSubstitutionProduit"></param>
        [HttpGet("list/{idSubstitutionProduit}")]
        public IActionResult GetListAllSubstitutionProduitsById(int idSubstitutionProduit)
        {
            try
            {
                var substitutionproduits = _repWrapper.SubstitutionProduit.GetListAllSubstitutionProduits(idSubstitutionProduit);
                if (substitutionproduits == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idSubstitutionProduit);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(substitutionproduits);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) SubstitutionProduit en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idSubstitutionProduit"></param>
        [HttpGet("{idSubstitutionProduit}")]
        public IActionResult GetSubstitutionProduitById(int idSubstitutionProduit)
        {            
            try
            {
                var  substitutionproduit = _repWrapper.SubstitutionProduit.FindByCondition(o => o.IdSubstitutionProduit.Equals(idSubstitutionProduit)).FirstOrDefault();
                if (substitutionproduit==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idSubstitutionProduit);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idSubstitutionProduit);
                    return Ok(substitutionproduit);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) SubstitutionProduit
        /// </summary>
        /// <returns></returns>
        /// <param name="substitutionproduit"></param>
        [HttpPost]
        public IActionResult AjouterSubstitutionProduit([FromBody]SubstitutionProduit substitutionproduit)
        {
            try
            {
                if (substitutionproduit == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.SubstitutionProduit.Create(substitutionproduit);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, substitutionproduit.IdSubstitutionProduit.ToString());
                return Ok(substitutionproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) SubstitutionProduit
        /// </summary>
        /// <returns></returns>
        /// <param name="idsubstitutionproduit"></param>
        /// <param name="substitutionproduit"></param>
        [HttpPut("{idSubstitutionProduit}")]
        public IActionResult MAJSubstitutionProduit(int idsubstitutionproduit, [FromBody]SubstitutionProduit substitutionproduit)
        {
            try
            {
                if (substitutionproduit == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbsubstitutionproduit = _repWrapper.SubstitutionProduit.FindByCondition(o => o.IdSubstitutionProduit.Equals(idsubstitutionproduit)).FirstOrDefault();

                if (dbsubstitutionproduit ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idsubstitutionproduit);
                }                

                 _mapper.Map(substitutionproduit,dbsubstitutionproduit);
                _repWrapper.SubstitutionProduit.Update(dbsubstitutionproduit);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idsubstitutionproduit);
                return Ok(substitutionproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) SubstitutionProduit
        /// </summary>
        /// <returns></returns>
        /// <param name="idsubstitutionproduit"></param>
        [HttpDelete("{idSubstitutionProduit}")]
        public IActionResult SupprimerSubstitutionProduit(int idsubstitutionproduit)
        {
            try
            {                
                SubstitutionProduit substitutionproduitASupprimer = _repWrapper.SubstitutionProduit.FindByCondition(o => o.IdSubstitutionProduit == idsubstitutionproduit).FirstOrDefault();

                if (substitutionproduitASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idsubstitutionproduit);
                }
                
                _repWrapper.SubstitutionProduit.Delete(substitutionproduitASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idsubstitutionproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de SubstitutionProduit
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetSubstitutionProduitCompte()
        {
            try
            {
                var compte = _repWrapper.SubstitutionProduit.Count();
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
	
