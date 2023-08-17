using API.Cadastro.Clientes.DKP.Data.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Cadastro.Clientes.DKP.Data.Model
{
    [Table("TB_Cliente")]
    public class ClienteModel
    {
        public ClienteModel() { }
        public ClienteModel(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public DateTime DataInicio { get; set; }
        public bool Status { get; set; }

        #region Validação

        [NotMapped]
        public string MensagemErro { get; private set; }
        [NotMapped]
        private int CnpjLength = 14;

        public bool IsValid()
        {
            MensagemErro = "";

            if (!ValidacaoHelper.CnpjValido(Cnpj) ||
                !Cnpj.EUmNumero() ||
                !(Cnpj.HasLength(CnpjLength))
            )
            {
                MensagemErro = $"O CNPJ {Cnpj} não é válido.";
                return false;
            }

            return true;
        }

        #endregion
    }
}