namespace Dominio.Models;

public class Orcamento
{
    public int Id { get; set; } // ID do orçamento
    public  string? NumeroOrcamento { get; set; } // Número do orçamento
    public  DateTime DataDeEmissao { get; set; } // Data de emissão do orçamento
    public  DateTime DataDeValidade { get; set; } // Data de validade do orçamento
    public  int[] IDservico  { get; set; } = null!; // IDs dos serviços associados ao orçamento
    public  int IDCliente { get; set; } // ID do cliente associado ao orçamento
    public  Servico Servico { get; set; } = null!; // Serviço associado ao orçamento
    public  decimal ValorTotal { get; set; } // Valor total do orçamento
    public  string? Status { get; set; } // Status do orçamento (Pendente, Aprovado, Rejeitado)
}
