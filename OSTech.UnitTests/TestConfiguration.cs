using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace OSTech.Tests;

public static class TestConfiguration
{
    private static readonly IConfiguration Configuration =
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .Build();

    public static string ConnectionString =>
        Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException(
            "DefaultConnection não foi configurada."
        );
}
