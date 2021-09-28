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
    [Route("api/marque")]
    public class MarqueController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Marque";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public MarqueController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Marques avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/marque/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllMarquesPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Marque.GetListAllMarques()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Marque.GetListAllMarques().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Marques
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllMarques()
        {
            try
            {
                var marques = _repWrapper.Marque.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(marques);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Marques
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllMarques()
        {
            try
            {
                var marques = _repWrapper.Marque.GetListAllMarques();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(marques);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Marques en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idMarque"></param>
        [HttpGet("list/{idMarque}")]
        public IActionResult GetListAllMarquesById(int idMarque)
        {
            try
            {
                var marques = _repWrapper.Marque.GetListAllMarques(idMarque);
                if (marques == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idMarque);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(marques);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Marque en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idMarque"></param>
        [HttpGet("{idMarque}")]
        public IActionResult GetMarqueById(int idMarque)
        {            
            try
            {
                var  marque = _repWrapper.Marque.FindByCondition(o => o.IdMarque.Equals(idMarque)).FirstOrDefault();
                if (marque==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idMarque);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idMarque);
                    return Ok(marque);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Marque
        /// </summary>
        /// <returns></returns>
        /// <param name="marque"></param>
        [HttpPost]
        public IActionResult AjouterMarque([FromBody]Marque marque)
        {
            try
            {
                if (marque == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Marque.Create(marque);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, marque.IdMarque.ToString());
                return Ok(marque);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Marque
        /// </summary>
        /// <returns></returns>
        /// <param name="idmarque"></param>
        /// <param name="marque"></param>
        [HttpPut("{idMarque}")]
        public IActionResult MAJMarque(int idmarque, [FromBody]Marque marque)
        {
            try
            {
                if (marque == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbmarque = _repWrapper.Marque.FindByCondition(o => o.IdMarque.Equals(idmarque)).FirstOrDefault();

                if (dbmarque ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idmarque);
                }                

                 _mapper.Map(marque,dbmarque);
                _repWrapper.Marque.Update(dbmarque);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idmarque);
                return Ok(marque);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Marque
        /// </summary>
        /// <returns></returns>
        /// <param name="idmarque"></param>
        [HttpDelete("{idMarque}")]
        public IActionResult SupprimerMarque(int idmarque)
        {
            try
            {                
                Marque marqueASupprimer = _repWrapper.Marque.FindByCondition(o => o.IdMarque == idmarque).FirstOrDefault();

                if (marqueASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idmarque);
                }
                
                _repWrapper.Marque.Delete(marqueASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idmarque);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Marque
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetMarqueCompte()
        {
            try
            {
                var compte = _repWrapper.Marque.Count();
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
	
