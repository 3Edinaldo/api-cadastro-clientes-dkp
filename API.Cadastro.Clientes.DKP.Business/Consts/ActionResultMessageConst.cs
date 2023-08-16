namespace API.Cadastro.Clientes.DKP.Business.Consts
{
    public static class ActionResultMessageConst
    {
        public const string
            IdNaoEncontrado = "Cliente de id {0} não encontrado!",
            CnpjNaoEncontrado = "Cliente de cnpj {0} não encontrado!",
            IdExcluido = "Cliente de id {0} excluído!",
            CnpjExcluido = "Cliente de cnpj {0} excluído!",
            ClientesNaoEncontrado = "Clientes não encontrado!",
            SemDadosParaApresentacao = "A requisiçao não retornou nenhum dado!";

        public static string SetMessageValue(this string message, dynamic value)
        {
            return string.Format(message, value);
        }
    }
}
