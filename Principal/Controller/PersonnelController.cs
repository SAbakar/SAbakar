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
    [Route("api/personnel")]
    public class PersonnelController : Controller 
    {
        #region Declarions, DI & autres

        private readonly string NomEntite = "Personnel";        
        private readonly IRepositoryWrapper _repWrapper;
        private readonly IMessageNotification _msgeNotification;                
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PersonnelController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMessageNotification messageNotification, IUriService uriService)
        {
            _msgeNotification = messageNotification;
            _repWrapper = repositoryWrapper;
            _mapper = mapper; 
            _uriService = uriService;
        }

        #endregion

        /// <summary>
        /// Afficher la liste de toutes les Personnels avec pagination
        /// </summary>
        /// <returns></returns>        
        [HttpGet("pages")]//exemple:  api/personnel/pages?pageNumber=1&pageSize=10
        public IActionResult GetAllPersonnelsPages([FromQuery] PaginationFilter filter)
        {
            try
            {                
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = _repWrapper.Personnel.GetListAllPersonnels()
                       .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                       .Take(validFilter.PageSize)
                       .ToList();
                var totalRecords = _repWrapper.Personnel.GetListAllPersonnels().Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route);
                return Ok(pagedReponse);                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }
        
        /// <summary>
        /// Afficher tous les Personnels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllPersonnels()
        {
            try
            {
                var personnels = _repWrapper.Personnel.FindAll();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(personnels);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de tous les Personnels
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetListAllPersonnels()
        {
            try
            {
                var personnels = _repWrapper.Personnel.GetListAllPersonnels();
                _msgeNotification.EntiteRetournee($"{NomEntite}s");
                return Ok(personnels);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher la liste de Personnels en fonction de  l'Id
        /// </summary>
        /// <returns></returns>
        /// <param name="idPersonnel"></param>
        [HttpGet("list/{idPersonnel}")]
        public IActionResult GetListAllPersonnelsById(int idPersonnel)
        {
            try
            {
                var personnels = _repWrapper.Personnel.GetListAllPersonnels(idPersonnel);
                if (personnels == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idPersonnel);
                }
                else
                {                    
                    _msgeNotification.EntiteRetournee($"{NomEntite}s");
                    return Ok(personnels);
                }                
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName,String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Afficher un(e) Personnel en particulier
        /// </summary>
        /// <returns></returns>
        /// <param name="idPersonnel"></param>
        [HttpGet("{idPersonnel}")]
        public IActionResult GetPersonnelById(int idPersonnel)
        {            
            try
            {
                var  personnel = _repWrapper.Personnel.FindByCondition(o => o.IdPersonnel.Equals(idPersonnel)).FirstOrDefault();
                if (personnel==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idPersonnel);
                }
                else
                {
                    _msgeNotification.EntiteRetourneeParID(NomEntite, idPersonnel);
                    return Ok(personnel);
                }
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Creer un(e) Personnel
        /// </summary>
        /// <returns></returns>
        /// <param name="personnel"></param>
        [HttpPost]
        public IActionResult AjouterPersonnel([FromBody]Personnel personnel)
        {
            try
            {
                if (personnel == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                _repWrapper.Personnel.Create(personnel);
                _repWrapper.Save();                
                _msgeNotification.EntiteCreee(NomEntite, personnel.IdPersonnel.ToString());
                return Ok(personnel);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Mise à jour d'un(e) Personnel
        /// </summary>
        /// <returns></returns>
        /// <param name="idpersonnel"></param>
        /// <param name="personnel"></param>
        [HttpPut("{idPersonnel}")]
        public IActionResult MAJPersonnel(int idpersonnel, [FromBody]Personnel personnel)
        {
            try
            {
                if (personnel == null)
                {
                    return _msgeNotification.EntiteNull(NomEntite);
                }

                if (!ModelState.IsValid)
                {
                    return _msgeNotification.EntiteNonValide(NomEntite);
                }
                
                var dbpersonnel = _repWrapper.Personnel.FindByCondition(o => o.IdPersonnel.Equals(idpersonnel)).FirstOrDefault();

                if (dbpersonnel ==null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite,idpersonnel);
                }                

                 _mapper.Map(personnel,dbpersonnel);
                _repWrapper.Personnel.Update(dbpersonnel);
                _repWrapper.Save();

                _msgeNotification.EntiteMiseAJour(NomEntite, idpersonnel);
                return Ok(personnel);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Suppression d'un(e) Personnel
        /// </summary>
        /// <returns></returns>
        /// <param name="idpersonnel"></param>
        [HttpDelete("{idPersonnel}")]
        public IActionResult SupprimerPersonnel(int idpersonnel)
        {
            try
            {                
                Personnel personnelASupprimer = _repWrapper.Personnel.FindByCondition(o => o.IdPersonnel == idpersonnel).FirstOrDefault();

                if (personnelASupprimer   == null)
                {
                    return _msgeNotification.EntiteNonTrouvee(NomEntite, idpersonnel);
                }
                
                _repWrapper.Personnel.Delete(personnelASupprimer);
                _repWrapper.Save();
                return _msgeNotification.EntiteSupprimee(NomEntite, idpersonnel);
            }
            catch (Exception ex)
            {
                return _msgeNotification.MessageErreur(ControllerContext.ActionDescriptor.ActionName, String.Concat(ex.Message,ex.InnerException==null?"":" - [Details] : ",ex.InnerException));
            }
        }

        /// <summary>
        /// Retourne le total d'enregistrement de Personnel
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetPersonnelCompte()
        {
            try
            {
                var compte = _repWrapper.Personnel.Count();
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
	
