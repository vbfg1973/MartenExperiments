namespace Cli
{
    using System.Globalization;
    using CsvHelper;

    public static class CsvHelpers
    {
        public static async IAsyncEnumerable<T> ReadCsv<T>(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecordsAsync<T>();

            await foreach (var record in records)
            {
                yield return record;
            }
        }
    }
}
