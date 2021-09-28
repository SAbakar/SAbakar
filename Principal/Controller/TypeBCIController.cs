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
    [Route("api/typebci")]
    public class TypeBCIController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "TypeBCI";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public TypeBCIController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les TypeBCIs avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/typebci/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllTypeBCIsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.TypeBCI.GetListAllTypeBCIs()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.TypeBCI.GetListAllTypeBCIs().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les TypeBCIs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllTypeBCIs()
        {
            try
            {
                var typebcis = _repWrapper.TypeBCI.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(typebcis);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les TypeBCIs
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllTypeBCIs()
        {
            try
            {
                var typebcis = _repWrapper.TypeBCI.GetListAllTypeBCIs();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(typebcis);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de TypeBCIs en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idTypeBCI"></param>
        [HttpGet("list/{idTypeBCI}")]
        public IActionResult GetListAllTypeBCIsById(int idTypeBCI)
        {
            try
            {
                var typebcis = _repWrapper.TypeBCI.GetListAllTypeBCIs(idTypeBCI);
                if (typebcis == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idTypeBCI);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(typebcis);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) TypeBCI en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idTypeBCI"></param>
        [HttpGet("{idTypeBCI}")]
        public IActionResult GetTypeBCIById(int idTypeBCI)
        {            
            try
            {
                var  typebci = _repWrapper.TypeBCI.FindByCondition(o => o.IdTypeBCI.Equals(idTypeBCI)).FirstOrDefault();
                if (typebci==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idTypeBCI);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idTypeBCI);
                    return Ok(typebci);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) TypeBCI
        /// </summary>
        /// <returns></returns>
        /// <param name="typebci"></param>
        [HttpPost]
        public IActionResult AjouterTypeBCI([FromBody]TypeBCI typebci)
        {
            try
            {
                if (typebci == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.TypeBCI.Create(typebci);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, typebci.IdTypeBCI.ToString());
                return Ok(typebci);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) TypeBCI
        /// </summary>
        /// <returns></returns>
        /// <param name="idtypebci"></param>
        /// <param name="typebci"></param>
        [HttpPut("{idTypeBCI}")]
        public IActionResult MAJTypeBCI(int idtypebci, [FromBody]TypeBCI typebci)
        {
            try
            {
                if (typebci == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbtypebci = _repWrapper.TypeBCI.FindByCondition(o => o.IdTypeBCI.Equals(idtypebci)).FirstOrDefault();

                if (dbtypebci ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idtypebci);
                }                

                 _mapper.Map(typebci,dbtypebci);
                _repWrapper.TypeBCI.Update(dbtypebci);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idtypebci);
                return Ok(typebci);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) TypeBCI
        /// </summary>
        /// <returns></returns>
        /// <param name="idtypebci"></param>
        [HttpDelete("{idTypeBCI}")]
        public IActionResult SupprimerTypeBCI(int idtypebci)
        {
            try
            {                
                TypeBCI typebciASupprimer = _repWrapper.TypeBCI.FindByCondition(o => o.IdTypeBCI == idtypebci).FirstOrDefault();

                if (typebciASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idtypebci);
                }
                
                _repWrapper.TypeBCI.Delete(typebciASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idtypebci);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de TypeBCI
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetTypeBCICompte()
        {
            try
            {
                var compte = _repWrapper.TypeBCI.Count();
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
	
