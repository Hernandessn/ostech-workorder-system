namespace OSTech.EFCore
{
    public static class AppConfig
    {
        public static string GetConnectionString()
        {
            return "Server=localhost;Database=OSTechDatabase;User=root;Password=SENHA_REMOVIDA;";
        }
    }
}