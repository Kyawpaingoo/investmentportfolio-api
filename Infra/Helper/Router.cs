using Microsoft.Extensions.Configuration;

namespace Infra;

public struct ConnectionString
{
    public static string GetConnectionString()
    {
        return 
            "Host=ep-proud-water-a5jia66v-pooler.us-east-2.aws.neon.tech;Database=neondb;Username=neondb_owner;Password=1TULrKAgqwz6";
    }
}