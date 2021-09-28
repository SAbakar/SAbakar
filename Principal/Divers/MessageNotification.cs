using Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using Repositories;

namespace Principal.Divers
{
    public interface IMessageNotification
    {
        IActionResult MessageErreur(string ActionName, string DetailErreur);
        IActionResult Information(string Message);
        IActionResult EntiteNull(string NomEntite);
        IActionResult EntiteNonValide(string NomEntite);
        void EntiteRetournee(string NomEntite);
        IActionResult EntiteCreee(string NomEntite, string Valeur);
        IActionResult EntiteCreeeListe(string NomEntite, string Valeur);
        IActionResult EntiteNonTrouvee(string NomEntite, int id);
        IActionResult EntiteNonTrouvee(string NomEntite, string id);
        void EntiteRetourneeParID(string NomEntite, int id);
        IActionResult EntiteMiseAJour(string NomEntite, int id);
        IActionResult EntiteMiseAJour(string NomEntite, string lin);
        IActionResult EntiteSupprimee(string NomEntite, int id);
        IActionResult EntiteNonSuprimable(string NomEntite, int id, string Raison);
        IActionResult BatchEntitesMiseAJour(string NomEntite, string ids);
        void EntiteCompte(string NomEntite);
    }

    public class MessageNotification : ControllerBase, IMessageNotification
    {
        private readonly ILoggerManager _logger;
        public MessageNotification(ILoggerManager loggerManager)
        {
            _logger = loggerManager;
        }
        public IActionResult MessageErreur(string ActionName, string DetailErreur)
        {
            _logger.LogError($"{Utilitaires.NameCurrentUser} Une erreur s'est produite dans l'action '{ActionName}'. [Message d'erreur] : <{ DetailErreur }>");
            return StatusCode(500, "Erreur de serveur interne.");
        }

        public IActionResult EntiteNull(string NomEntite)
        {
            _logger.LogErrorNull($"{Utilitaires.NameCurrentUser} {NomEntite}");
            return NotFound();
        }

        public IActionResult EntiteNonValide(string NomEntite)
        {
            _logger.LogErrorNonValide($"{Utilitaires.NameCurrentUser} {NomEntite}");
            return NotFound();
        }

        public void EntiteRetournee(string NomEntite)
        {
            _logger.LogInfo($"{Utilitaires.NameCurrentUser} Retourne la liste des [{NomEntite}]");
        }

        public void EntiteRetourneeParID(string NomEntite, int id)
        {
            _logger.LogInfo($"{Utilitaires.NameCurrentUser} Retourne l'entité [{NomEntite}] correspondant à l'Id '{id}'.");
        }

        public IActionResult EntiteCreee(string NomEntite, string Valeur)
        {
            _logger.LogInfo($"{Utilitaires.NameCurrentUser} Un nouvel enregistrement '{Valeur}' est créé dans l'entité [{NomEntite}]  avec succes!");
            return NoContent();
        }

        public IActionResult EntiteCreeeListe(string NomEntite, string Valeur)
        {
            _logger.LogInfo($"{Utilitaires.NameCurrentUser} Une serie d'enregistrement avec les valeurs '{Valeur}' sont créée dans l'entité [{NomEntite}]  avec succes!");
            return NoContent();
        }

        public IActionResult EntiteNonTrouvee(string NomEntite, int id)
        {
            _logger.LogError($"{Utilitaires.NameCurrentUser} L'entité [{NomEntite}] correspondant à l'id '{id}' n'existe pas.");
            return NotFound($"[{NomEntite}] avec le ID '{id}' n'existe pas. (code erreur : 404)");
        }
        
        public IActionResult EntiteNonTrouvee(string NomEntite, string lib)
        {
            _logger.LogError($"{Utilitaires.NameCurrentUser} L'entité [{NomEntite}] correspondant au nom '{lib}' n'existe pas.");
            return NotFound($"[{NomEntite}] avec le ID '{lib}' n'existe pas. (code erreur : 404)");
        }

        public IActionResult EntiteMiseAJour(string NomEntite, int id)
        {
            _logger.LogInfo($"{Utilitaires.NameCurrentUser} L'entité [{NomEntite}] avec le id '{id}' a été mise a jour.");
            return NoContent();
        }
         public IActionResult EntiteMiseAJour(string NomEntite, string lib)
        {
            _logger.LogInfo($"{Utilitaires.NameCurrentUser} L'entité [{NomEntite}] avec le nom '{lib}' a été mise a jour.");
            return NoContent();
        }

        public IActionResult BatchEntitesMiseAJour(string NomEntite, string ids)
        {
            _logger.LogInfo($"{Utilitaires.NameCurrentUser} L'entité [{NomEntite}] avec les ids '{ids}' a sont mises a jour.");
            return NoContent();
        }

        public IActionResult EntiteSupprimee(string NomEntite, int id)
        {
            _logger.LogInfo($"{Utilitaires.NameCurrentUser} L'entité [{NomEntite}] avec le id '{id}' a été supprimée avec succès!");
            return NoContent();
        }

        public IActionResult EntiteNonSuprimable(string NomEntite, int id, string Raison)
        {
            _logger.LogError($"{Utilitaires.NameCurrentUser} L'entité [{NomEntite}] avec le id '{id}' ne peut etre supprimé pour la raison suivante : '{Raison}'");
            return NoContent();
        }

        
        public void EntiteCompte(string NomEntite)
        {
            _logger.LogInfo(String.Concat($"{Utilitaires.NameCurrentUser} Retourne le total d'enregistrement de [{NomEntite}]"));
        }

        public IActionResult Information(string Message)
        {
            _logger.LogInfo($"{Utilitaires.NameCurrentUser} Message Info :" + Message);
            return Ok(Message);
        }
    }
}
