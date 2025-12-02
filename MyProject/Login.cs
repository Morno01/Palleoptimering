using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace MyProject
{
    public class BrugerDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string? PasswordHash { get; set; }
        public string? Navn { get; set; }
    }

    public class Login
    {
        private readonly string _connectionString;

        public Login(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("DefaultConnection not found in configuration");
        }

        public async Task<BrugerDto?> GetBrugerByEmailAsync(string email)
        {
            const string sql = "SELECT Id, , Password, Navn FROM Brugere WHERE  = @email";
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", email);

            await conn.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new BrugerDto
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    PasswordHash = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Navn = reader.IsDBNull(3) ? null : reader.GetString(3)
                };
            }

            return null;
        }

        // Simple validator: compares given password with stored hash.
        // Replace the comparison below with your hashing library (BCrypt, PBKDF2, etc.)
        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var bruger = await GetBrugerByEmailAsync(email);
            if (bruger == null || string.IsNullOrEmpty(bruger.PasswordHash)) return false;

            // TODO: Replace with secure hash verification
            // Example (if you store BCrypt hashes): return BCrypt.Net.BCrypt.Verify(password, bruger.PasswordHash);
            return bruger.PasswordHash == password; // <-- INSECURE fallback for testing only
        }
    }
}