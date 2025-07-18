namespace Dominio.Models;

public class Servico
{
    public  int Id { get; set; } // Nome do serviço
    public  string? Nome { get; set; } // Nome do serviço
    public  string? Descricao { get; set; } // Descrição do serviço
    public CategoriaServico CategoriaServico { get; set; } = null!; // Categoria do serviço
    public int IdCategoriaServico { get; set; } // ID da categoria de serviço associada
    public decimal Preco { get; set; } // Preço do serviço
}
