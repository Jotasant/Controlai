using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controlai.Applicacao.DTOs
{
    public class DtoCliente
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string? Condominio { get; set; }
        public string? bloco { get; set; }
        public string? apartamento { get; set; }
        public string? Rua { get; set; }
        public string? NumeroResidencial { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? CEP { get; set; }
        public string? Whatsapp { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Instagram { get; set; }
        public string? CPF { get; set; }
        public string? CNPJ { get; set; }

        public DtoCliente ( string Nome, 
                            DateTime DataDeCadastro = default, 
                            DateTime DataDeNascimento = default, 
                            string Condominio = "", 
                            string bloco = "", 
                            string apartamento = "",
                            string Rua = "",
                            string NumeroResidencial = "",
                            string Complemento = "",
                            string Bairro = "",
                            string Cidade = "",
                            string Estado = "",
                            string CEP = "",
                            string Whatsapp = "",
                            string Telefone = "",
                            string Email = "",
                            string Instagram = "",
                            string CPF = "",
                            string CNPJ = ""
                          )
        {
            this.Nome = Nome;
            this.DataDeCadastro = DataDeCadastro;
            this.DataDeNascimento = DataDeNascimento;
            this.Condominio = Condominio;
            this.bloco = bloco;
            this.apartamento = apartamento;
            this.Rua = Rua;
            this.NumeroResidencial = NumeroResidencial;
            this.Complemento = Complemento;
            this.Bairro = Bairro;
            this.Cidade = Cidade;
            this.Estado = Estado;
            this.CEP = CEP;
            this.Whatsapp = Whatsapp;
            this.Telefone = Telefone;
            this.Email = Email;
            this.Instagram = Instagram;
            this.CPF = CPF;
            this.CNPJ = CNPJ;
        }
    }
}