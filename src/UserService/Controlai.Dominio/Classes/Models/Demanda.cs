namespace Dominio.Models;


public class Demanda
{
    private string _nome = "";
    public int Id { get; set; } // ID do serviço
    public string Nome { get => _nome; set { Nome = string.IsNullOrWhiteSpace(value) ? throw new Exception("Nome Obrigatório") : value; } } // Nome do serviço
    public string? Descricao { get; set; } // Descrição do serviço
    public DateTime DataDeFechamento { get; set; } // Data de fechamento do serviço
    public DateTime DataDeConclusao { get; set; } // Data de conclusão do serviço
    public DateTime DataDeEntrega { get; set; } // Data de entrega do serviço
    public string? Atraso { get; set; } // dias
    public string? TipoCliente { get; set; } // Tipo de cliente: Pessoa Física, Pessoa Jurídica
    public string? ClientNR { get; set; } // Cliente Novo ou Recorrente
    public string? Status { get; set; } // Pendente, Em Andamento, Concluído, Entregue
    public int IDResponsavel { get; set; } // Nome do responsável pelo serviço
    public int IDOrcamento { get; set; } // ID do orçamento associado ao serviço
    public int IDServico { get; set; } // ID do serviço
    public decimal Custo { get; set; } // Custo do serviço
    public string? Val_MaoDeObra { get; set; } // Valor da mão de obra
    public string? Val_Servico { get; set; } // Valor do serviço
    public string? Desconto { get; set; } // Desconto aplicado "%"
    public int NumServico { get; set; } // Número do serviço 



}