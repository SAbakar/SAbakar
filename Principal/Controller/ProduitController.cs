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
    [Route("api/produit")]
    public class ProduitController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Produit";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public ProduitController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Produits avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/produit/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllProduitsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Produit.GetListAllProduits()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Produit.GetListAllProduits().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Produits
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllProduits()
        {
            try
            {
                var produits = _repWrapper.Produit.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(produits);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Produits
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllProduits()
        {
            try
            {
                var produits = _repWrapper.Produit.GetListAllProduits();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(produits);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Produits en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idProduit"></param>
        [HttpGet("list/{idProduit}")]
        public IActionResult GetListAllProduitsById(int idProduit)
        {
            try
            {
                var produits = _repWrapper.Produit.GetListAllProduits(idProduit);
                if (produits == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idProduit);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(produits);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Produit en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idProduit"></param>
        [HttpGet("{idProduit}")]
        public IActionResult GetProduitById(int idProduit)
        {            
            try
            {
                var  produit = _repWrapper.Produit.FindByCondition(o => o.IdProduit.Equals(idProduit)).FirstOrDefault();
                if (produit==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idProduit);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idProduit);
                    return Ok(produit);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Produit
        /// </summary>
        /// <returns></returns>
        /// <param name="produit"></param>
        [HttpPost]
        public IActionResult AjouterProduit([FromBody]Produit produit)
        {
            try
            {
                if (produit == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Produit.Create(produit);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, produit.IdProduit.ToString());
                return Ok(produit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Produit
        /// </summary>
        /// <returns></returns>
        /// <param name="idproduit"></param>
        /// <param name="produit"></param>
        [HttpPut("{idProduit}")]
        public IActionResult MAJProduit(int idproduit, [FromBody]Produit produit)
        {
            try
            {
                if (produit == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbproduit = _repWrapper.Produit.FindByCondition(o => o.IdProduit.Equals(idproduit)).FirstOrDefault();

                if (dbproduit ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idproduit);
                }                

                 _mapper.Map(produit,dbproduit);
                _repWrapper.Produit.Update(dbproduit);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idproduit);
                return Ok(produit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Produit
        /// </summary>
        /// <returns></returns>
        /// <param name="idproduit"></param>
        [HttpDelete("{idProduit}")]
        public IActionResult SupprimerProduit(int idproduit)
        {
            try
            {                
                Produit produitASupprimer = _repWrapper.Produit.FindByCondition(o => o.IdProduit == idproduit).FirstOrDefault();

                if (produitASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idproduit);
                }
                
                _repWrapper.Produit.Delete(produitASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Produit
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetProduitCompte()
        {
            try
            {
                var compte = _repWrapper.Produit.Count();
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
	
