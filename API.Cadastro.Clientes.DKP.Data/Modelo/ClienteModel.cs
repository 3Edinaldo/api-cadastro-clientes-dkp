using API.Cadastro.Clientes.DKP.Data.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public string CNPJ { get; set; }
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

            if (!ValidacaoHelper.CnpjValido(CNPJ) ||
                !CNPJ.EUmNumero() ||
                !(CNPJ.HasLength(CnpjLength))
            )
            {
                MensagemErro = $"O CNPJ {CNPJ} não é válido.";
                return false;
            }

            return true;
        }

        #endregion
    }
}