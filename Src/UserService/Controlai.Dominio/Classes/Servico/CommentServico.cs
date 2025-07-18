/*
namespace Dominio.Models;

public class Servico
{
    public required string Nome { get; set; } // Nome do serviço
    public required string Descricao { get; set; } // Descrição do serviço
    public CategoriaServico CategoriaServico { get; set; } = null!; // Categoria do serviço
    public int IdCategoriaServico { get; set; } // ID da categoria de serviço associada
    public decimal Preco { get; set; } // Preço do serviço
}

public class CategoriaServico
{
    public int id { get; set; } // ID da categoria
    public string? Nome { get; set; } // Nome da categoria
    public string? Descricao { get; set; } // Descrição da categoria
}

public class AtributoCategoriaServico
{
    public int id { get; set; } // ID do atributo
    public string? Nome { get; set; } // Nome do atributo
    public string? Descricao { get; set; } // Descrição do atributo
    public int IdCategoriaServico { get; set; } // ID da categoria de serviço associada

    public CategoriaServico CategoriaServico { get; set; } = null!; // Categoria de serviço associada
}

public class ValorAtributoServico
{
    public int id { get; set; } // ID do valor do atributo
    public int IdCategoriaServico { get; set; } // ID da categoria de serviço associada
    public CategoriaServico CategoriaServico { get; set; } = null!; // Categoria de serviço associada
    public int IdAtributoCategoriaServico { get; set; } // ID do atributo de categoria de serviço associado

    public AtributoCategoriaServico AtributoCategoriaServico { get; set; } = null!; // Atributo de categoria de serviço associado

    public string? Valor { get; set; } // Valor do atributo
}
*/