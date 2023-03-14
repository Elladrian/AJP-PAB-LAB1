using AJPPABLAB1;
using System.Data;
using System.Diagnostics;

static async Task BulkCopySaveAll(List<Kody> kody)
{
    DataTable sourceData = new DataTable();
    sourceData.Columns.Add("kod_pocztowy");
    sourceData.Columns.Add("adres");
    sourceData.Columns.Add("miejscowosc");
    sourceData.Columns.Add("wojewodztwo");
    sourceData.Columns.Add("powiat");

    for (int i = 0; i < kody.Count; i++)
    {
        sourceData.Rows.Add(new object[] { kody[i].kod_pocztowy, kody[i], kody[i].miejscowosc, kody[i].wojewodztwo, kody[i].powiat });
    }

    Stopwatch stopwatch = new Stopwatch();
    TimeSpan sampleTimeSpan = new TimeSpan();
    int samples = 10;

    for (int i = 1; i <= samples; i++)
    {
        await ClearTable();

        stopwatch.Start();
        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
        {
            bulkCopy.DestinationTableName = "dbo.Kody_pocztowe";
            await bulkCopy.WriteToServerAsync(sourceData);
        }
        stopwatch.Stop();
        sampleTimeSpan += stopwatch.Elapsed;
        Console.WriteLine($"[{i}] SQLBulkCopy Saving Time is {stopwatch.Elapsed}");

        stopwatch.Restart();
    }

    sampleTimeSpan = sampleTimeSpan.Divide(samples);
    Console.WriteLine($"Mean SQLBulkCopy Saving Time is {sampleTimeSpan}");
}