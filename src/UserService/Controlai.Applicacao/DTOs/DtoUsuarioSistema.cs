namespace Applicacao.DTOs;

using Dominio.Enums;

public class DtoUsuarioSistema
{

    public int Id { get; set; } // ID do usuário
    public string? Nome { get; set; } // Nome completo do usuário
    public bool isAdministrador { get; set; } // Perfil do usuário: Administrador, Usuário Comum, etc.
    public string? Usuario { get; set; } // Nome de usuário
    public DateTime DataDeCadastro { get; set; } // Data de cadastro do usuário
    public string? Senha { get; set; } // Senha do usuário
    public string? Email { get; set; } // Email do usuário  
    public TipoDeBusca Busca { get; set; } // Tipo de busca para filtrar usuários

    public DtoUsuarioSistema(int Id = 0, string Nome = "", string Usuario = "", string Senha = "", string Email = "", bool isAdministrador = false, DateTime DataDeCadastro = default)
    {
        this.Id = Id;
        this.Nome = Nome;
        this.isAdministrador = isAdministrador;
        this.Usuario = Usuario;
        this.DataDeCadastro = DataDeCadastro;
        this.Senha = Senha;
        this.Email = Email;
        
    }
    
    
        
}



