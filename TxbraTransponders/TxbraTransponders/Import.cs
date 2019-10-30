using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utilities;

namespace TxBraTransponders
{
    public static class Import
    {
        private const string TxBraRentalSheetId = "1rdnpl3TlwK2TYRFim-HQaGTaGqrqCmWK3THM3hbh_u8";
        private const string TxBraOwnedSheetId = "1rdnpl3TlwK2TYRFim-HQaGTaGqrqCmWK3THM3hbh_u8";

        public static List<RentalTransponder> RentalsFromCsv(Stream stream) =>
            CsvFiles
            .GetRecords<RentalTransponder, RentalsCsvImportMap>(stream)
            .Select(CleanImportData)
            .ToList();

        public static List<RentalTransponder> RentalsFromSheets() =>
            GoogleSheets
            .GetValues(TxBraRentalSheetId, "RENTAL!B2:D")
            .Select(RentalsSheetsImportMap)
            .Select(CleanImportData)
            .ToList();

        public static List<OwnedTransponder> OwnedFromCsv(Stream stream) =>
            CsvFiles
            .GetRecords<OwnedTransponder, OwnedCsvImportsMap>(stream)
            .Select(CleanImportData)
            .ToList();

        public static IEnumerable<OwnedTransponder> OwnedFromSheets() =>
            GoogleSheets
            .GetValues(TxBraOwnedSheetId, "MASTER!A2:D")
            .Select(OwnedSheetsImportMap)
            .Select(CleanImportData)
            .ToList();

        private static OwnedTransponder CleanImportData(OwnedTransponder transponder) =>
            new OwnedTransponder
            {
                LicensePhone = transponder.LicensePhone.Trim().TrimStart('0'),
                TransponderId = transponder.TransponderId.Trim(),
                FirstName = transponder.FirstName.Trim(),
                LastName = transponder.LastName.Trim()
            };

        private static RentalTransponder CleanImportData(RentalTransponder transponder) =>
            new RentalTransponder
            {
                Owner = transponder.Owner.Trim(),
                StickerId = transponder.StickerId.Trim(),
                TransponderId = transponder.TransponderId.Trim()
            };

        private sealed class OwnedCsvImportsMap : ClassMap<OwnedTransponder>
        {
            public OwnedCsvImportsMap()
            {
                Map(transponder => transponder.LicensePhone).Name("License/Phone");
                Map(transponder => transponder.TransponderId).Name("Transponder");
                Map(transponder => transponder.FirstName).Name("FirstName");
                Map(transponder => transponder.LastName).Name("LastName");
            }
        }

        private sealed class RentalsCsvImportMap : ClassMap<RentalTransponder>
        {
            public RentalsCsvImportMap()
            {
                Map(transponder => transponder.Owner).Name("Owner");
                Map(transponder => transponder.StickerId).Name("StickerID");
                Map(transponder => transponder.SortId).Name("SortID");
                Map(transponder => transponder.TransponderId).Name("SerialID");
            }
        }

        private static RentalTransponder RentalsSheetsImportMap(IList<object> row) =>
            new RentalTransponder
            {
                StickerId = row[0].ToString(),
                TransponderId = row[1].ToString(),
                Owner = row[2].ToString()
            };

        private static OwnedTransponder OwnedSheetsImportMap(IList<object> row) =>
             new OwnedTransponder
             {
                 LicensePhone = row[0].ToString().Trim().TrimStart('0'),
                 TransponderId = row[1].ToString(),
                 FirstName = row[2].ToString().Trim(),
                 LastName = row[3].ToString().Trim()
             };
    }
}