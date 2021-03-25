using System;
using System.Collections.Generic;

namespace KursWalutNBPLib
{
    /// <summary>
    /// Klasa przechowująca pobrane dane na temat kursu danej waluty i posiadająca metody służące do prowadzenia na nich podstawowych obliczeń.
    /// </summary>
    public class ExchangeInfo
    {
        private List<ExchangeInfoModel> data = new();

        /// <summary>
        /// Liczba raportów, które udało się pobrać z danego okresu.
        /// </summary>
        public int DataCount => data.Count;

        /// <summary>
        /// Pełna nazwa waluty.
        /// </summary>
        public string CurrencyName => data[0].CurrencyName;

        /// <summary>
        /// Trzyliterowy kod waluty.
        /// </summary>
        public string Currency { get; }

        /// <summary>
        /// Data początkowa, od której pobierane są dane.
        /// </summary>
        public DateTime DateFrom { get; }

        /// <summary>
        /// Data końcowa, do której pobierane są dane.
        /// </summary>
        public DateTime DateTo { get; }

        /// <summary>
        /// Średni kurs w podanym okresie.
        /// </summary>
        public decimal AvgaverageExchangeRate
        {
            get
            {
                decimal sum = 0;
                int count = 0;

                foreach(ExchangeInfoModel d in data)
                {
                    sum += d.AverageExchangeRate;
                    count++;
                }

                return sum / count;
            }
        }

        /// <summary>
        /// Najniższy średni kurs dzienny w danym okresie.
        /// </summary>
        public decimal MinExchangeRate
        {
            get
            {
                decimal? min = null;

                foreach (ExchangeInfoModel d in data)
                    if (min == null || min > d.AverageExchangeRate)
                        min = d.AverageExchangeRate;

                return (decimal)min;
            }
        }

        /// <summary>
        /// Data najniższego średniego kursu dziennego w danym okresie.
        /// </summary>
        public DateTime MinExchangeRateDate
        {
            get
            {
                decimal? min = null;
                DateTime? dateTime = null;

                foreach (ExchangeInfoModel d in data)
                {
                    if (min == null || min > d.AverageExchangeRate)
                    {
                        min = d.AverageExchangeRate;
                        dateTime = d.Date;
                    }
                }

                return (DateTime)dateTime;
            }
        }

        /// <summary>
        /// Najwyższy średni kurs dzienny w danym okresie.
        /// </summary>
        public decimal MaxExchangeRate
        {
            get
            {
                decimal? max = null;

                foreach (ExchangeInfoModel d in data)
                    if (max == null || max < d.AverageExchangeRate)
                        max = d.AverageExchangeRate;

                return (decimal)max;
            }
        }

        /// <summary>
        /// Data najwyższego średniego kursu dziennego w danym okresie.
        /// </summary>
        public DateTime MaxExchangeRateDate
        {
            get
            {
                decimal? max = null;
                DateTime? dateTime = null;

                foreach (ExchangeInfoModel d in data)
                {
                    if (max == null || max < d.AverageExchangeRate)
                    {
                        max = d.AverageExchangeRate;
                        dateTime = d.Date;
                    }
                }

                return (DateTime)dateTime;
            }
        }

        /// <summary>
        /// Konstruktor klasy ExchangeInfo.
        /// </summary>
        /// <param name="currency">Trzyliterowy kod waluty</param>
        /// <param name="dateFrom">Data początkowa, od której ma zacząć się pobieranie danych</param>
        /// <param name="dateTo">Data końcowa, do której mają być pobierane dane</param>
        public ExchangeInfo(string currency, DateTime dateFrom, DateTime dateTo)
        {
            Currency = CheckCurrencyFormat(currency);
            DateFrom = dateFrom;
            DateTo = dateTo;

            data = new DataDownloader().GetDataFromDateRange(currency, dateFrom, dateTo);

            if (data.Count == 0)
                throw new ArgumentNullException();
        }

        public static string CheckCurrencyFormat(string input)
        {
            if (int.TryParse(input, out _) || input.Length != 3)
                throw new FormatException();

            return input.ToUpper();
        }
    }
}