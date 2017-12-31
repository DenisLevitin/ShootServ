using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ClosedXML.Excel;

namespace OpenXmlHelpers
{
    public class OpenXmlHelper
    {

        /// <summary>
        /// Получить Excel - list
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="listNum"></param>
        /// <returns></returns>
        public static IXLWorksheet GetWorksheet(Stream stream, int listNum)
        {
            var wb = new XLWorkbook(stream);

            return wb.Worksheets.ToList()[listNum - 1];
        }

        /// <summary>
        /// Получить ячейки
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="listNum"></param>
        /// <param name="rangeBegin"></param>
        /// <param name="rangeEnd"></param>
        /// <returns></returns>
        public static IXLRanges GetRanges(Stream stream, int listNum, string rangeBegin, string rangeEnd)
        {
            var wb = new XLWorkbook(stream);

            var worksheet = wb.Worksheets.ToList()[listNum - 1];
            return worksheet.Ranges(string.Format("{0}:{1}", rangeBegin, rangeEnd));
        }

        /// <summary>
        /// Сохранить XL
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="wb"></param>
        public static void Save(Stream stream, XLWorkbook wb)
        {
            wb.SaveAs(stream);
        }

        
    }
}
