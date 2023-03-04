using System.Data.SqlClient;

namespace AJPPABLAB1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = @"Server=LOCALHOST\LOCALDATABASE;Database=TestDatabase;Trusted_Connection=True";
            string sqlExpression = "SELECT * FROM Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if(reader.HasRows)
                {
                    string columnName1 = reader.GetName(0);
                    string columnName2 = reader.GetName(1);
                    string columnName3 = reader.GetName(2);

                    Console.WriteLine($"{columnName1}\t{columnName2}\t{columnName3}");

                    while(await reader.ReadAsync())
                    {
                        var id = reader.GetValue(0);
                        var name = reader.GetValue(1);
                        var age = reader.GetValue(2);

                        Console.WriteLine($"{id}\t{name}\t{age}");
                    }
                }
            }
        }
    }
}