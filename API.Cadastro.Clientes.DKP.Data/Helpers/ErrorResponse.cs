using System.Reflection;

namespace API.Cadastro.Clientes.DKP.Data.Helpers
{
    public class ErrorResponse
    {
        public string Menssagem { get; set; }
        public MethodBase Metodo { get; set; }
    }
}