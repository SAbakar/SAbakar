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
    [Route("api/stockproduit")]
    public class StockProduitController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "StockProduit";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public StockProduitController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les StockProduits avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/stockproduit/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllStockProduitsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.StockProduit.GetListAllStockProduits()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.StockProduit.GetListAllStockProduits().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les StockProduits
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllStockProduits()
        {
            try
            {
                var stockproduits = _repWrapper.StockProduit.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(stockproduits);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les StockProduits
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllStockProduits()
        {
            try
            {
                var stockproduits = _repWrapper.StockProduit.GetListAllStockProduits();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(stockproduits);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de StockProduits en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idStockProduit"></param>
        [HttpGet("list/{idStockProduit}")]
        public IActionResult GetListAllStockProduitsById(int idStockProduit)
        {
            try
            {
                var stockproduits = _repWrapper.StockProduit.GetListAllStockProduits(idStockProduit);
                if (stockproduits == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idStockProduit);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(stockproduits);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) StockProduit en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idStockProduit"></param>
        [HttpGet("{idStockProduit}")]
        public IActionResult GetStockProduitById(int idStockProduit)
        {            
            try
            {
                var  stockproduit = _repWrapper.StockProduit.FindByCondition(o => o.IdStockProduit.Equals(idStockProduit)).FirstOrDefault();
                if (stockproduit==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idStockProduit);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idStockProduit);
                    return Ok(stockproduit);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) StockProduit
        /// </summary>
        /// <returns></returns>
        /// <param name="stockproduit"></param>
        [HttpPost]
        public IActionResult AjouterStockProduit([FromBody]StockProduit stockproduit)
        {
            try
            {
                if (stockproduit == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.StockProduit.Create(stockproduit);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, stockproduit.IdStockProduit.ToString());
                return Ok(stockproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) StockProduit
        /// </summary>
        /// <returns></returns>
        /// <param name="idstockproduit"></param>
        /// <param name="stockproduit"></param>
        [HttpPut("{idStockProduit}")]
        public IActionResult MAJStockProduit(int idstockproduit, [FromBody]StockProduit stockproduit)
        {
            try
            {
                if (stockproduit == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbstockproduit = _repWrapper.StockProduit.FindByCondition(o => o.IdStockProduit.Equals(idstockproduit)).FirstOrDefault();

                if (dbstockproduit ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idstockproduit);
                }                

                 _mapper.Map(stockproduit,dbstockproduit);
                _repWrapper.StockProduit.Update(dbstockproduit);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idstockproduit);
                return Ok(stockproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) StockProduit
        /// </summary>
        /// <returns></returns>
        /// <param name="idstockproduit"></param>
        [HttpDelete("{idStockProduit}")]
        public IActionResult SupprimerStockProduit(int idstockproduit)
        {
            try
            {                
                StockProduit stockproduitASupprimer = _repWrapper.StockProduit.FindByCondition(o => o.IdStockProduit == idstockproduit).FirstOrDefault();

                if (stockproduitASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idstockproduit);
                }
                
                _repWrapper.StockProduit.Delete(stockproduitASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idstockproduit);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de StockProduit
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetStockProduitCompte()
        {
            try
            {
                var compte = _repWrapper.StockProduit.Count();
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
	
