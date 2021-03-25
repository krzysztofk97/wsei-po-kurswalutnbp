using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;

namespace KursWalutNBPLib
{
    /// <summary>
    /// Klasa zawierąca metody pobierające dane o walutach z serwera.
    /// </summary>
    public class DataDownloader
    {
        private WebClient webClient = new();

        private string FileListURL => Helper.FilesListURL;
        private string FileURL => Helper.FilesURL;

        private List<string> GetFilesList()
        {
            List<string> result = new();

            using (Stream data = webClient.OpenRead(FileListURL))
            using (StreamReader reader = new(data))
            {
                while (!reader.EndOfStream)
                    result.Add(reader.ReadLine());
            }

            return result;
        }

        private List<string> GetFilesListFromDateRange(DateTime dateFrom, DateTime dateTo)
        {
            DateTime tmpDateFrom = dateFrom;

            List<string> filesList = GetFilesList();
            List<string> result = new();

            while (tmpDateFrom <= dateTo)
            {
                Regex regexTime = new(@"a\d\d\dz" + tmpDateFrom.ToString("yyMMdd"));

                List<string> match = filesList.Where(x => regexTime.IsMatch(x)).ToList();
                result.AddRange(match);

                tmpDateFrom = tmpDateFrom.AddDays(1);
            }

            return result;
        }

        private ExchangeInfoModel GetData(string fileName, string currency)
        {
            string rawData;

            using (Stream data = webClient.OpenRead(FileURL + fileName + ".xml"))
            using (StreamReader reader = new(data))
            {
                reader.ReadLine();
                rawData = reader.ReadToEnd();
            }

            XDocument document = XDocument.Parse(rawData);

            XElement xElement = document.Root;

            var dataLinq = xElement.Elements("pozycja")
                .Where(x => x.Element("kod_waluty").Value == currency.ToString())
                .Select(y => new ExchangeInfoModel(
                    y.Element("nazwa_waluty").Value,
                    y.Element("kod_waluty").Value,
                    decimal.Parse(y.Element("kurs_sredni").Value, new CultureInfo("pl-PL")),
                    GetDateFromFileName(fileName)));

            return dataLinq.First();
        }

        private List<ExchangeInfoModel> GetDataFromFilesList(List<string> fileNames, string currency)
        {
            List<ExchangeInfoModel> result = new();

            foreach (string fileName in fileNames)
                result.Add(GetData(fileName, currency));

            return result;
        }

        /// <summary>
        /// Pobiera dane o walucie, z dni w podanym okresie.
        /// </summary>
        /// <param name="currency">Trzyliterowy kod waluty</param> 
        /// <param name="dateFrom">Data początkowa</param>
        /// <param name="dateTo">Data końcowa</param>
        /// <returns>Lista obiektów typu ExchangeInfoModel zawierających dane o walucie w danym dniu</returns>
        public List<ExchangeInfoModel> GetDataFromDateRange(string currency, DateTime dateFrom, DateTime dateTo) => GetDataFromFilesList(GetFilesListFromDateRange(dateFrom, dateTo), currency);

        private DateTime GetDateFromFileName(string fileName)
        {
            string dateString = fileName.Split('z')[1];

            return DateTime.ParseExact(dateString, "yyMMdd", CultureInfo.InvariantCulture);
        }
    }
}