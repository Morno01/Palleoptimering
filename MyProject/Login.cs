using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace MyProject
{
    public class BrugerDto
    {
        public int Id { get; set; }
        public string Brugernavn { get; set; } = "";
        public string? Password { get; set; }
        public string? Navn { get; set; }
    }

    public class Login
    {
        private readonly string _connectionString;

        public Login(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration), "DefaultConnection not found in configuration");
        }

        public async Task<BrugerDto?> GetBrugerByBrugernavnAsync(string brugernavn)
        {
            const string sql = "SELECT Id, Brugernavn, Password, Navn FROM Brugere WHERE Brugernavn = @brugernavn";
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@brugernavn", brugernavn);

            await conn.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new BrugerDto
                {
                    Id = reader.GetInt32(0),
                    Brugernavn = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Password = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Navn = reader.IsDBNull(3) ? null : reader.GetString(3)
                };
            }

            return null;
        }

        public async Task<bool> ValidateCredentialsAsync(string brugernavn, string password)
        {
            var bruger = await GetBrugerByBrugernavnAsync(brugernavn);
            if (bruger == null || string.IsNullOrEmpty(bruger.Password)) return false;

            // IMPORTANT: replace this with secure hashed verification in production.
            // Example: install BCrypt.Net-Next and use BCrypt.Verify(password, bruger.Password)
            return bruger.Password == password;
        }
    }
}