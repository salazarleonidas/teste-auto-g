using gestao_produtos.Infrastructures.Helper;

namespace gestao_produtos.Infrastructures
{
    public sealed class ConnectionStrings : IAppOptions
    {
        public static string ConfigSectionPath => "ConnectionStrings";

        public string Database { get; set; }

        public string Collation { get; set; }
    }
}
