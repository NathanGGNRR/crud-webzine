namespace Webzine.Common
{
    public static class StringExtension
    {
        /// <summary>
        /// Découpe une chaine de caractère et ajoute des "..." à la fin.
        /// </summary>
        /// <param name="content">Chaîne de caractère a modifier</param>
        /// <param name="length">Taille maximum avant découpage</param>
        /// <returns>Chaîne de caractère traitée</returns>
        public static string SubstringEndSuspension(this string content, int length)
        {
            return (content.Length > length) ? content.Substring(0, length) + "..." : content;
        }

    }
}
