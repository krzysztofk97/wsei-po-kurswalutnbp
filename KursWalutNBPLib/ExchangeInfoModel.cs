using System;

namespace KursWalutNBPLib
{
    /// <summary>
    /// Klasa zawierająca dane z pojedynczego raportu.
    /// </summary>
    public class ExchangeInfoModel
    {
        /// <summary>
        /// Pełna nazwa waluty.
        /// </summary>
        public string CurrencyName { get; }

        /// <summary>
        /// Trzyliterowy kod waluty.
        /// </summary>
        public string Currency { get; }

        /// <summary>
        /// Kurs średni.
        /// </summary>
        public decimal AverageExchangeRate { get; }

        /// <summary>
        /// Data raportu.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Konstruktor klasy ExchangeInfoModel.
        /// </summary>
        /// <param name="currencyName">Pełna nazwa waluty</param>
        /// <param name="currency">Trzyliterowy kod waluty</param>
        /// <param name="averageExchangeRate">Kurs średni</param>
        /// <param name="date">Data raportu</param>
        public ExchangeInfoModel(string currencyName, string currency, decimal averageExchangeRate, DateTime date)
        {
            CurrencyName = currencyName;
            Currency = currency;
            AverageExchangeRate = averageExchangeRate;
            Date = date;
        }
    }
}
