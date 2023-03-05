using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Data.SqlClient;
using System.Globalization;
using System.Diagnostics;
//using AJPPABLAB1.Models;
using Microsoft.EntityFrameworkCore;

namespace AJPPABLAB1
{
    internal class Program
    {
        static List<Kody> imported_kody = new List<Kody>();
        static string connectionString = @"Server=LOCALHOST\LOCALDATABASE;Database=AJPPABLAB1;Trusted_Connection=True";

        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            //Ajppablab1Context _context = new Ajppablab1Context();

            stopwatch.Start();
            await importCSV();
            stopwatch.Stop();
            Console.WriteLine($"Elapsed Import Time is {stopwatch.Elapsed}");
            stopwatch.Restart();

            #region saving by record
            const int samples = 1;
            Stopwatch meanRecordStopwatch = new Stopwatch();
            TimeSpan recordTimeSpan = new TimeSpan();
            TimeSpan sampleTimeSpan = new TimeSpan();
            for (int i = 1; i <= samples; i++)
            {
                await ClearTable();

                stopwatch.Start();
                foreach (var record in imported_kody)
                {
                    meanRecordStopwatch.Start();
                    await SaveOneRecord(record);
                    meanRecordStopwatch.Stop();

                    recordTimeSpan += meanRecordStopwatch.Elapsed;
                    meanRecordStopwatch.Restart();
                }
                stopwatch.Stop();
                sampleTimeSpan += stopwatch.Elapsed;
                stopwatch.Restart();
            }
            sampleTimeSpan = sampleTimeSpan.Divide(samples);
            recordTimeSpan = recordTimeSpan.Divide(imported_kody.Count * samples);
            Console.WriteLine($"Mean Saving by one Record Time is {sampleTimeSpan}");
            Console.WriteLine($"Mean Record Saving Time is {recordTimeSpan}\n");
            #endregion

            //await SaveAllCollection(imported_kody);

            //await EFsaveOneRecord(imported_kody, _context);
        }

        static async Task ClearTable()
        {
            string sqlExpression = "DELETE FROM Kody_pocztowe";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                await command.ExecuteNonQueryAsync();
            }
        }

        //static async Task EFsaveAllCollection(List<Kody> kody, Ajppablab1Context context)
        //{
        //    const int samples = 1;
        //    Stopwatch sampleStopwatch = new Stopwatch();
        //    Stopwatch recordStopwatch = new Stopwatch();
        //    TimeSpan recordTimeSpan = new TimeSpan();
        //    TimeSpan sampleTimeSpan = new TimeSpan();

        //    for (int i = 1; i <= samples; i++)
        //    {
        //        await ClearTable();

        //        sampleStopwatch.Start();
        //        foreach (var kod in kody)
        //        {
        //            recordStopwatch.Start();

        //            //context.KodyPocztowes.Add(data);
        //            //context.SaveChanges();

        //            recordStopwatch.Stop();
        //            recordTimeSpan += recordStopwatch.Elapsed;
        //            recordStopwatch.Restart();
        //        }
        //        sampleStopwatch.Stop();
        //        sampleTimeSpan += sampleStopwatch.Elapsed;
        //        sampleStopwatch.Restart();
        //    }

        //    sampleTimeSpan = sampleTimeSpan.Divide(samples);
        //    recordTimeSpan = recordTimeSpan.Divide(imported_kody.Count * samples);
        //    Console.WriteLine($"Mean Saving by one Record Time is {sampleTimeSpan}");
        //    Console.WriteLine($"Mean Record Saving Time is {recordTimeSpan}\n");
        //}

        //static async Task EFsaveOneRecord(List<Kody> kody, Ajppablab1Context context)
        //{
        //    const int samples = 1;
        //    Stopwatch sampleStopwatch = new Stopwatch();
        //    Stopwatch recordStopwatch = new Stopwatch();
        //    TimeSpan recordTimeSpan = new TimeSpan();
        //    TimeSpan sampleTimeSpan = new TimeSpan();
   
        //    for (int i = 1; i <= samples; i++)
        //    {
        //        await ClearTable();

        //        sampleStopwatch.Start();
        //        foreach (var kod in kody)
        //        {
        //            recordStopwatch.Start();

        //            //context.KodyPocztowes.Add(data);
        //            //context.SaveChanges();

        //            recordStopwatch.Stop();
        //            recordTimeSpan += recordStopwatch.Elapsed;
        //            recordStopwatch.Restart();
        //        }
        //        sampleStopwatch.Stop();
        //        sampleTimeSpan += sampleStopwatch.Elapsed;
        //        sampleStopwatch.Restart();
        //    }
            
        //    sampleTimeSpan = sampleTimeSpan.Divide(samples);
        //    recordTimeSpan = recordTimeSpan.Divide(imported_kody.Count * samples);
        //    Console.WriteLine($"Mean Saving by one Record Time is {sampleTimeSpan}");
        //    Console.WriteLine($"Mean Record Saving Time is {recordTimeSpan}\n");
        //}

        static async Task SaveOneRecord(Kody kody)
        {
            string sqlExpression = "INSERT INTO Kody_Pocztowe (Kod_pocztowy, Adres, Miejscowosc, Wojewodztwo, Powiat) Values (@Kod_pocztowy, @Adres, @Miejscowosc, @Wojewodztwo, @Powiat)";

            using (SqlConnection connection = new SqlConnection( connectionString ))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter kod_pocztowyParameter = new SqlParameter("@Kod_pocztowy", kody.kod_pocztowy);
                SqlParameter adresParameter = new SqlParameter("@Adres", kody.adres);
                SqlParameter miejscowoscParameter = new SqlParameter("@Miejscowosc", kody.miejscowosc);
                SqlParameter wojewodztwoParameter = new SqlParameter("@Wojewodztwo", kody.wojewodztwo);
                SqlParameter powiatParameter = new SqlParameter("@Powiat", kody.powiat);

                command.Parameters.Add(kod_pocztowyParameter);
                command.Parameters.Add(adresParameter);
                command.Parameters.Add(miejscowoscParameter);
                command.Parameters.Add(wojewodztwoParameter);
                command.Parameters.Add(powiatParameter);

                await command.ExecuteNonQueryAsync();
            }
        }

        static async Task SaveAllCollection(List<Kody> kody)
        {
            const int samples = 1;
            Stopwatch stopwatch = new Stopwatch();

            Stopwatch meanRecordStopwatch = new Stopwatch();
            TimeSpan recordTimeSpan = new TimeSpan();
            TimeSpan sampleTimeSpan = new TimeSpan();

            string sqlExpression = "INSERT INTO Kody_Pocztowe (Kod_pocztowy, Adres, Miejscowosc, Wojewodztwo, Powiat) Values (@Kod_pocztowy, @Adres, @Miejscowosc, @Wojewodztwo, @Powiat)";

            for(int i = 1; i <= samples; i++)
            {
                await ClearTable();
                stopwatch.Start();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    foreach (var kod in kody)
                    {
                        meanRecordStopwatch.Start();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);

                        SqlParameter kod_pocztowyParameter = new SqlParameter("@Kod_pocztowy", kod.kod_pocztowy);
                        SqlParameter adresParameter = new SqlParameter("@Adres", kod.adres);
                        SqlParameter miejscowoscParameter = new SqlParameter("@Miejscowosc", kod.miejscowosc);
                        SqlParameter wojewodztwoParameter = new SqlParameter("@Wojewodztwo", kod.wojewodztwo);
                        SqlParameter powiatParameter = new SqlParameter("@Powiat", kod.powiat);

                        command.Parameters.Add(kod_pocztowyParameter);
                        command.Parameters.Add(adresParameter);
                        command.Parameters.Add(miejscowoscParameter);
                        command.Parameters.Add(wojewodztwoParameter);
                        command.Parameters.Add(powiatParameter);

                        await command.ExecuteNonQueryAsync();

                        meanRecordStopwatch.Stop();
                        recordTimeSpan += meanRecordStopwatch.Elapsed;
                        meanRecordStopwatch.Restart();
                    }
                }
                stopwatch.Stop();
                sampleTimeSpan += stopwatch.Elapsed;
                stopwatch.Restart();
            }

            sampleTimeSpan = sampleTimeSpan.Divide(samples);
            recordTimeSpan = recordTimeSpan.Divide(imported_kody.Count * samples);
            Console.WriteLine($"Mean Saving by one Record Time is {sampleTimeSpan}");
            Console.WriteLine($"Mean Record Saving Time is {recordTimeSpan}\n");
        }

        static async Task importCSV()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord= true,
                Delimiter = ";",
                MemberTypes = MemberTypes.Properties,
                HeaderValidated = null,
                MissingFieldFound = null,
            };

            using(var reader = new StreamReader(@"C:\Users\oleks\Projects\ajp\AJP-PAB-LAB1\kody.csv"))
            using(var csv = new CsvReader(reader, csvConfig))
            {
                imported_kody = csv.GetRecords<Kody>().ToList();
                Console.WriteLine($"Records Imported: {imported_kody.Count}");
            }
        }
    }

    public class Kody {
        [Name("KOD POCZTOWY")]
        public string kod_pocztowy { get; set; } = "";
        [Name("ADRES")]
        public string adres { get; set; } = "";
        [Name("MIEJSCOWOŚĆ")]
        public string miejscowosc { get; set; } = "";
        [Name("WOJEWÓDZTWO")]
        public string wojewodztwo { get; set; } = "";
        [Name("POWIAT")]
        public string powiat { get; set; } = "";
    }
}