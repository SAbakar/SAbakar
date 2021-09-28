using System;

namespace Repositories
{
    public static class Utilitaires
    {
        public static string NameCurrentUser = "";

        public static int[] GetAgeYearMonth(DateTime dateOfBirth) {
            var today = DateTime.Now;
            var birthDate = dateOfBirth;
            var age = today.Year - birthDate.Year;
            var m = today.Month - birthDate.Month;

            if (m< 0 || (m == 0 && today.Day < birthDate.Day)) {
              age--;
            }
            int[] res = new int[]  { 0,0};

            res[0]= Math.Abs(age); res[1] =Math.Abs(m);
            return res;
          }

        public enum EnumEtatTicket
        {
            EnAttente = 1,
            EnCours,
            Prochain,
            Pause,
            Termine,
            Annuler
        }

        public enum EnumEtatTermine
        {
            Traite=1,
            Annuler
        }

        public enum EnumTypeTicket
        {
            Normal = 1,
            Interne,
            VIP
        }
    }
}
