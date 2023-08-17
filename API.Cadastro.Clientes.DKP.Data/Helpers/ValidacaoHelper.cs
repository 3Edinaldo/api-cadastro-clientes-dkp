using System;
using System.Linq;

namespace API.Cadastro.Clientes.DKP.Data.Helpers
{
    public static class ValidacaoHelper
    {
        /// <summary>
        /// Verificar se CNPJ é válido 
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns>bool</returns>
        public static bool CnpjValido(string vrCNPJ)
        {
            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(CNPJ.Substring(nrDig, 1));

                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));

                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));

                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);

                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
                }

                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Verificar se a string contém apenas número
        /// </summary>
        /// <param name="value"></param>
        /// <returns>bool</returns>
        public static bool EUmNumero(this string value)
        {
            return value.All(Char.IsDigit);
        }
        /// <summary>
        /// Remove os imbolos (.,/,-) da string 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string TratarCNPJ(this string value)
        {
            string valorTratado = value.Replace(".", "");
            valorTratado = value.Replace("/", "");
            valorTratado = value.Replace("-", "");
            return valorTratado;
        }
        /// <summary>
        /// Convert um valor bool para int
        /// </summary>
        /// <param name="value"></param>
        /// <returns>int</returns>
        public static int ToInt(this bool value)
        {
            return (value ? 1 : 0);
        }
        public static bool ExceededCharacters(this string value, int minValue, int maxValue)
        {
            return (value.Length < minValue || value.Length > maxValue);
        }
        public static bool HasLength(this string value, int length)
        {
            return (value.Length == length);
        }
    }
}