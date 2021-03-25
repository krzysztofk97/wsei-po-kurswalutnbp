using System;
using KursWalutNBPLib;
using System.Net;

namespace KursWalutNBP
{
    class Program
    {
        private static DateTime? ParseDate(string input, bool dateFrom = true)
        {
            DateTime? date = null;

            if(dateFrom)
            {
                try
                {
                    date = DateTime.Parse(input);
                }
                catch(FormatException)
                {
                    Console.WriteLine("Data początkowa ma błedny format lub jest spoza zakresu");
                }
                catch(Exception)
                {
                    Console.WriteLine("Wystąpił błąd podczas przetwarzania daty początkowej");
                }
            }
            else
            {
                try
                {
                    date = DateTime.Parse(input);
                }
                catch(FormatException)
                {
                    Console.WriteLine("Data końcowa ma błedny format lub jest spoza zakresu");
                }
                catch(Exception)
                {
                    Console.WriteLine("Wystąpił błąd podczas przetwarzania daty końcowej");
                }
            }

            return date;
        }

        static void Main(string[] args)
        {
            string currency = null;
            DateTime? dateFrom = null;
            DateTime? dateTo = null;

            if (args.Length >= 1)
            {
                try
                {
                    currency = ExchangeInfo.CheckCurrencyFormat(args[0]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Nieprawidłowy format waluty.");
                }
            }
                
            if (args.Length >= 2)
                dateFrom = ParseDate(args[1]); 

            if (args.Length == 3)
            {
                dateTo = ParseDate(args[2], dateFrom: false);

                if (dateTo < dateFrom)
                {
                    Console.WriteLine("Data końcowa nie może być wcześniejsza niż data początkowa");
                    dateTo = null;
                }
            } 

            while (currency == null)
            {
                Console.Write("Podaj trzyliterowy kod waluty (np. EUR): ");

                try
                {
                    currency = ExchangeInfo.CheckCurrencyFormat(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Nieprawidłowy format waluty.");
                }
            }

            Console.WriteLine("Wybrana waluta to: " + currency.ToString());

            while (dateFrom == null)
            {
                Console.Write("Podaj datę początkową: ");
                dateFrom = ParseDate(Console.ReadLine());
            }
                

            Console.WriteLine("Wybrana data początkowa: " + ((DateTime)dateFrom).ToString("dd.MM.yyyy"));

            while (dateTo == null)
            {
                Console.Write("Podaj datę końcową: ");
                dateTo = ParseDate(Console.ReadLine(), dateFrom: false);

                if(dateTo < dateFrom)
                {
                    Console.WriteLine("Data końcowa nie może być wcześniejsza niż data początkowa");
                    dateTo = null;
                }
            }
                
            Console.WriteLine("Wybrana data końcowa: " + ((DateTime)dateTo).ToString("dd.MM.yyyy"));

            Console.WriteLine();

            try
            {
                ExchangeInfo exchangeInfo = new(currency, (DateTime)dateFrom, (DateTime)dateTo);
                Console.WriteLine($"Dane dla {currency} ({exchangeInfo.CurrencyName}) w okresie od {(DateTime)dateFrom:dd.MM.yyyy} do {(DateTime)dateTo:dd.MM.yyyy}");
                Console.WriteLine($"Kurs średni: {exchangeInfo.AvgaverageExchangeRate:0.0000}");
                Console.WriteLine($"Najniższy dzienny średni kurs: {exchangeInfo.MinExchangeRate:0.0000} (w dniu {exchangeInfo.MinExchangeRateDate:dd.MM.yyyy})");
                Console.WriteLine($"Najwyższy dzienny średni kurs: {exchangeInfo.MaxExchangeRate:0.0000} (w dniu {exchangeInfo.MaxExchangeRateDate:dd.MM.yyyy})");
                Console.WriteLine();

                if(exchangeInfo.DataCount == 1)
                    Console.WriteLine($"Dane na podstawie 1 raportu ze strony Nardowego Banku Polskiego.");
                else
                    Console.WriteLine($"Dane na podstawie {exchangeInfo.DataCount} raportów ze strony Narodowego Banku Polskiego.");
            }
            catch(ArgumentNullException)
            {
                Console.WriteLine($"Brak danych dla waluty o kodzie {currency} w podanym okresie.");
            }
            catch(WebException)
            {
                Console.WriteLine("Nie można połączyć się z serwerem, sprawdź czy napewno masz połaczenie z internetem i spróbuj ponownie.");
            }
        }
    }
}
