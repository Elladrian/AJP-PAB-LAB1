using Dapper;
using System.Diagnostics;

namespace AJPPABLAB1.ModelsDapper
{
    public class KodyPocztoweRepository : IKodyPocztoweRepository
    {
        private readonly DapperContext _context;

        public KodyPocztoweRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateKodPocztowy(KodyPocztowe kodyPocztowe)
        {
            var query = "INSERT INTO Kody_pocztowe (kod_pocztowy, adres, miejscowosc, wojewodztwo, powiat) VALUES (@kod_pocztowy, @adres, @miejscowosc, @wojewodztwo, @powiat)";

            var parameters = new DynamicParameters();
            parameters.Add("kod_pocztowy", kodyPocztowe.kod_pocztowy);
            parameters.Add("adres", kodyPocztowe.adres);
            parameters.Add("miejscowosc", kodyPocztowe.miejscowosc);
            parameters.Add("wojewodztwo", kodyPocztowe.wojewodztwo);
            parameters.Add("powiat", kodyPocztowe.powiat);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task CreateKodyPocztowe(List<KodyPocztowe> kodyPocztowe)
        {
            Stopwatch recordStopwatch = new Stopwatch();

            using (var connection = _context.CreateConnection())
            {
                foreach (var kod in kodyPocztowe)
                {
                    recordStopwatch.Start();
                    var query = "INSERT INTO Kody_pocztowe (kod_pocztowy, adres, miejscowosc, wojewodztwo, powiat) VALUES (@kod_pocztowy, @adres, @miejscowosc, @wojewodztwo, @powiat)";

                    var parameters = new DynamicParameters();

                    parameters.Add("kod_pocztowy", kod.kod_pocztowy);
                    parameters.Add("adres", kod.adres);
                    parameters.Add("miejscowosc", kod.miejscowosc);
                    parameters.Add("wojewodztwo", kod.wojewodztwo);
                    parameters.Add("powiat", kod.powiat);

                    await connection.ExecuteAsync(query, parameters);
                    recordStopwatch.Stop();

                    recordStopwatch.Restart();
                }
            }
        }

        public async Task<IEnumerable<KodyPocztowe>> GetKodyPocztowe()
        {
            var query = "SELECT * FROM Kody_pocztowe";

            using(var connection = _context.CreateConnection())
            {
                var kody_pocztowe = await connection.QueryAsync<KodyPocztowe>(query);
                return kody_pocztowe.ToList();
            }
        }
    }
}
