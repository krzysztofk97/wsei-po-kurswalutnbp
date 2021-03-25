using System;
using System.Configuration;

namespace KursWalutNBPLib
{
    /// <summary>
    /// Klasa zawierająca metody pomocnicze.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Adres URL do pliku z listą dostępnych na serwerze plików z kursami walut.
        /// </summary>
        public static string FilesListURL => ConfigurationManager.AppSettings["FilesListURL"].ToString();

        /// <summary>
        /// Adres URL serwera z plikami z kursami walut.
        /// </summary>
        public static string FilesURL => ConfigurationManager.AppSettings["FilesURL"].ToString();
    }
}
