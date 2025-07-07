namespace Dominio.Models;

public class UsuarioSistema
{
    public int Id { get; set; } // ID do usuário
    public string? Nome { get; set; }  // Nome completo do usuário
    public bool isAdministrador { get; set; } // Perfil do usuário: Administrador, Usuário Comum, etc.
    public string? Usuario { get; set; } // Nome de usuário
    public DateTime DataDeCadastro { get; set; } // Data de cadastro do usuário
    public string? SenhaHash { get; set; } // Senha do usuário
    public string? Email { get; set; } // Email do usuário

    private UsuarioSistema() { }

    public UsuarioSistema(string Nome, string Usuario, string SenhaHash, string Email)
    {
        this.Nome = Nome;
        this.Usuario = Usuario;
        this.Email = Email;

        DataDeCadastro = DateTime.Now;

        this.SenhaHash = SenhaHash;

    }

}
