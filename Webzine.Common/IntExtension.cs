using System;

namespace Webzine.Common
{
    public static class IntExtension
    {
        /// <summary>
        /// Fait le formatage des secondes (int) en minutes:secondes (string): (ex: 75 => 1:15).
        /// </summary>
        /// <param name="duration">Durée en seconde à formatter</param>
        /// <returns>La durée en minutes:secondes</returns>
        public static string ConvertToHM(this int duration)
        {
            return String.Concat(duration / 60 < 10 ? String.Concat("0", duration / 60) : (duration / 60).ToString(), ":", duration % 60 < 10 ? String.Concat("0", duration % 60) : (duration % 60).ToString());
        }
    }
}
