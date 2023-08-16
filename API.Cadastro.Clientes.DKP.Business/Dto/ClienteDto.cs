using API.Cadastro.Clientes.DKP.Data.Model;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Cadastro.Clientes.DKP.Business.Dto
{
    public class ClienteRequest
    {
        private const int CnpjLength = 14;
        private const int DefaultLength = 50;

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(CnpjLength, ErrorMessage = "O {0} não é válido.")]
        [MinLength(CnpjLength, ErrorMessage = "O {0} não é válido.")]
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(DefaultLength, ErrorMessage = "Número de caracteres excedido.")]
        public string RazaoSocial { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(DefaultLength, ErrorMessage = "Número de caracteres excedido.")]
        public string NomeFantasia { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Status { get; set; }
    }

    public class ClienteDto
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("CNPJ")]
        public string CNPJ { get; set; }
        [JsonProperty("RazaoSocial")]
        public string RazaoSocial { get; set; }
        [JsonProperty("NomeFantasia")]
        public string NomeFantasia { get; set; }
        [JsonProperty("DataInicio")]
        public DateTime DataInicio { get; set; }
        [JsonProperty("Status")]
        public bool Status { get; set; }

        public static implicit operator ClienteDto(ClienteModel clienteModel)
            => clienteModel == null ? new ClienteDto() : new ClienteDto
            {
                Id = clienteModel.Id,
                CNPJ = clienteModel.CNPJ,
                RazaoSocial = clienteModel.RazaoSocial,
                NomeFantasia = clienteModel.NomeFantasia,
                DataInicio = clienteModel.DataInicio,
                Status = clienteModel.Status
            };
    }
}