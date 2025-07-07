namespace Dominio.Models;


public class ValorAtributoServico
{
    public int id { get; set; } // ID do valor do atributo
    public string? NomeCategoria { get; set; }
    public int IdCategoriaServico { get; set; } // ID da categoria de serviço associada
    public CategoriaServico CategoriaServico { get; set; } = null!; // Categoria de serviço associada
    public int IdAtributoCategoriaServico { get; set; } // ID do atributo de categoria de serviço associado

    public AtributoCategoriaServico AtributoCategoriaServico { get; set; } = null!; // Atributo de categoria de serviço associado

    public string? Valor { get; set; } // Valor do atributo
}