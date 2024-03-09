using Npgsql;
namespace WebApplication9.Domain;

public class ConnectionDatabase
{
    private string? _connectionString;

    public ConnectionDatabase(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}