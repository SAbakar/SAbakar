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
    [Route("api/produitcommandefournisseur")]
    public class ProduitCommandeFournisseurController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "ProduitCommandeFournisseur";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public ProduitCommandeFournisseurController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les ProduitCommandeFournisseurs avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/produitcommandefournisseur/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllProduitCommandeFournisseursPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.ProduitCommandeFournisseur.GetListAllProduitCommandeFournisseurs()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.ProduitCommandeFournisseur.GetListAllProduitCommandeFournisseurs().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les ProduitCommandeFournisseurs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllProduitCommandeFournisseurs()
        {
            try
            {
                var produitcommandefournisseurs = _repWrapper.ProduitCommandeFournisseur.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(produitcommandefournisseurs);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les ProduitCommandeFournisseurs
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllProduitCommandeFournisseurs()
        {
            try
            {
                var produitcommandefournisseurs = _repWrapper.ProduitCommandeFournisseur.GetListAllProduitCommandeFournisseurs();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(produitcommandefournisseurs);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de ProduitCommandeFournisseurs en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idProduitCommandeFournisseur"></param>
        [HttpGet("list/{idProduitCommandeFournisseur}")]
        public IActionResult GetListAllProduitCommandeFournisseursById(int idProduitCommandeFournisseur)
        {
            try
            {
                var produitcommandefournisseurs = _repWrapper.ProduitCommandeFournisseur.GetListAllProduitCommandeFournisseurs(idProduitCommandeFournisseur);
                if (produitcommandefournisseurs == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idProduitCommandeFournisseur);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(produitcommandefournisseurs);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) ProduitCommandeFournisseur en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idProduitCommandeFournisseur"></param>
        [HttpGet("{idProduitCommandeFournisseur}")]
        public IActionResult GetProduitCommandeFournisseurById(int idProduitCommandeFournisseur)
        {            
            try
            {
                var  produitcommandefournisseur = _repWrapper.ProduitCommandeFournisseur.FindByCondition(o => o.IdProduitCommandeFournisseur.Equals(idProduitCommandeFournisseur)).FirstOrDefault();
                if (produitcommandefournisseur==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idProduitCommandeFournisseur);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idProduitCommandeFournisseur);
                    return Ok(produitcommandefournisseur);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) ProduitCommandeFournisseur
        /// </summary>
        /// <returns></returns>
        /// <param name="produitcommandefournisseur"></param>
        [HttpPost]
        public IActionResult AjouterProduitCommandeFournisseur([FromBody]ProduitCommandeFournisseur produitcommandefournisseur)
        {
            try
            {
                if (produitcommandefournisseur == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.ProduitCommandeFournisseur.Create(produitcommandefournisseur);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, produitcommandefournisseur.IdProduitCommandeFournisseur.ToString());
                return Ok(produitcommandefournisseur);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) ProduitCommandeFournisseur
        /// </summary>
        /// <returns></returns>
        /// <param name="idproduitcommandefournisseur"></param>
        /// <param name="produitcommandefournisseur"></param>
        [HttpPut("{idProduitCommandeFournisseur}")]
        public IActionResult MAJProduitCommandeFournisseur(int idproduitcommandefournisseur, [FromBody]ProduitCommandeFournisseur produitcommandefournisseur)
        {
            try
            {
                if (produitcommandefournisseur == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbproduitcommandefournisseur = _repWrapper.ProduitCommandeFournisseur.FindByCondition(o => o.IdProduitCommandeFournisseur.Equals(idproduitcommandefournisseur)).FirstOrDefault();

                if (dbproduitcommandefournisseur ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idproduitcommandefournisseur);
                }                

                 _mapper.Map(produitcommandefournisseur,dbproduitcommandefournisseur);
                _repWrapper.ProduitCommandeFournisseur.Update(dbproduitcommandefournisseur);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idproduitcommandefournisseur);
                return Ok(produitcommandefournisseur);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) ProduitCommandeFournisseur
        /// </summary>
        /// <returns></returns>
        /// <param name="idproduitcommandefournisseur"></param>
        [HttpDelete("{idProduitCommandeFournisseur}")]
        public IActionResult SupprimerProduitCommandeFournisseur(int idproduitcommandefournisseur)
        {
            try
            {                
                ProduitCommandeFournisseur produitcommandefournisseurASupprimer = _repWrapper.ProduitCommandeFournisseur.FindByCondition(o => o.IdProduitCommandeFournisseur == idproduitcommandefournisseur).FirstOrDefault();

                if (produitcommandefournisseurASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idproduitcommandefournisseur);
                }
                
                _repWrapper.ProduitCommandeFournisseur.Delete(produitcommandefournisseurASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idproduitcommandefournisseur);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de ProduitCommandeFournisseur
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetProduitCommandeFournisseurCompte()
        {
            try
            {
                var compte = _repWrapper.ProduitCommandeFournisseur.Count();
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
	
