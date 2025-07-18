namespace Dominio.Models;

public class AtributoCategoriaServico
{
    public int id { get; set; } // ID do atributo
    public string? Nome { get; set; } // Nome do atributo
    public string? Descricao { get; set; } // Descrição do atributo
    public int IdCategoriaServico { get; set; } // ID da categoria de serviço associada

    public CategoriaServico CategoriaServico { get; set; } = null!; // Categoria de serviço associada
}
